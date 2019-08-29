using Saritasa.JiraDayIssues.Domain.Issues.Builders;
using Saritasa.JiraDayIssues.Domain.Issues.Dtos;
using Saritasa.JiraDayIssues.Domain.Issues.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saritasa.JiraDayIssues.Domain.Issues.Reporters
{
    /// <summary>
    /// JIRA reporter class.
    /// </summary>
    public class JiraReporter
    {
        private readonly JiraService jiraService;

        /// <summary>
        /// Constructor class.
        /// </summary>
        public JiraReporter(JiraService jiraService)
        {
            this.jiraService = jiraService;
        }

        /// <summary>
        /// The method returns the daily report.
        /// </summary>
        /// <returns>Daily report.</returns>
        public async Task<string> GetDailyReportAsync(string username, DateTime date)
        {
            List<IssueDto> issues = await jiraService.GetIssuesAsync(username, date);

            string report = JiraReportBuilder.BuildStringReport(issues);

            return report;
        }
    }
}
