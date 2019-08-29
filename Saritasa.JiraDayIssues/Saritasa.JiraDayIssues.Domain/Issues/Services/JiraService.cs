using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using RestSharp;
using RestSharp.Authenticators;
using Saritasa.JiraDayIssues.Domain.Issues.Dtos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Saritasa.JiraDayIssues.Domain.Issues.Services
{
    /// <summary>
    /// Class for working with the JIRA API.
    /// </summary>
    public class JiraService
    {
        private const string JiraUrl = "https://saritasa.atlassian.net";
        private const string SearchApi = "/rest/api/latest/search";
        private const string BaseIssueApi = "/rest/api/latest/issue";

        private readonly RestClient client;
        private IRestResponse response;

        private static readonly Logger logger = LogManager.GetLogger("NLog.config");

        /// <summary>
        /// Constructor class.
        /// </summary>
        /// <param name="login">JIRA user login.</param>
        /// <param name="password">JIRA user password.</param>
        public JiraService(string login, string password)
        {
            if (login == null)
            {
                throw new ArgumentNullException("Login should not be null");
            }
            else if (password == null)
            {
                throw new ArgumentNullException("Password should not be null");
            }

            // Create client.
            client = new RestClient(JiraUrl);
            client.Authenticator = new HttpBasicAuthenticator(login, password);
            client.UseJson();
        }

        /// <summary>
        /// Method to get valid username for JQL.
        /// </summary>
        /// <returns>Valid username for JQL.</returns>
        private string GetValidUsernameForJQL(string username)
        {
            return username.Replace("@", "\\u0040");
        }

        /// <summary>
        /// Method to get JIRA issues.
        /// </summary>
        /// <returns>Issues.</returns>
        public async Task<List<IssueDto>> GetIssuesAsync(string username, DateTime date, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Create request with parameters.
            var issuesRequest = new RestRequest(SearchApi, Method.GET);
            issuesRequest.AddParameter("jql", $"worklogDate = {date.Date.ToString("yyyy-MM-dd")} AND worklogAuthor = {GetValidUsernameForJQL(username)}");
            issuesRequest.AddParameter("fields", "project,key,summary,timeoriginalestimate,worklog");

            logger.Info("Request {json} has been sent.", JsonConvert.SerializeObject(new { Method = issuesRequest.Method, Url = JiraUrl, Api = SearchApi }));

            response = await client.ExecuteGetTaskAsync(issuesRequest, cancellationToken);

            logger.Info("Response {json} was received.", JsonConvert.SerializeObject(new { ContentType = response.ContentType, Content = response.Content, StatusCode = response.StatusCode }));

            // Create json object.
            JObject jIssues = JObject.Parse(response.Content);

            // List of issues.
            List<IssueDto> issues = new List<IssueDto>();

            // Get issues from JSON file.
            foreach (var jField in jIssues.SelectTokens("issues").Values())
            {
                IssueDto issue = new IssueDto
                {
                    ProjectKey = jField.SelectToken("fields.project.key").Value<string>(),
                    ProjectName = jField.SelectToken("fields.project.name").Value<string>(),
                    IssueKey = jField.SelectToken("key").Value<string>(),
                    Summary = jField.SelectToken("fields.summary").Value<string>(),
                    TimeEstimateSeconds = jField.SelectToken("fields.timeoriginalestimate").Value<int?>()
                };

                var worklogsRequest = new RestRequest(BaseIssueApi + $@"/{issue.IssueKey}/worklog", Method.GET);

                logger.Info("Request {json} has been sent.", JsonConvert.SerializeObject(new { Method = issuesRequest.Method, Url = JiraUrl, Api = SearchApi }));

                response = await client.ExecuteGetTaskAsync(worklogsRequest, cancellationToken);

                logger.Info("Response {json} was received.", JsonConvert.SerializeObject(new { ContentType = response.ContentType, Content = response.Content, StatusCode = response.StatusCode }));

                JObject jWorklogs = JObject.Parse(response.Content);

                // Get worklogs of issues.
                foreach (var jWorklog in jWorklogs.SelectTokens("worklogs").Values())
                {
                    if (jWorklog.SelectToken("author.emailAddress").Value<string>() == username
                        && jWorklog.SelectToken("started").Value<DateTime>().Date == date.Date)
                    {
                        issue.Worklogs.Add(new WorklogDto
                        {
                            Comment = jWorklog.SelectToken("comment").Value<string>(),
                            TimeSpentSeconds = jWorklog.SelectToken("timeSpentSeconds").Value<int>()
                        });
                    }
                }

                if (issue.Worklogs.Count != 0)
                {
                    issues.Add(issue);
                }
            }

            logger.Info("Get issues and worklogs for user {username} and date {date}", username, date.ToString("yyyy-MM-dd"));

            return issues;
        }
    }
}
