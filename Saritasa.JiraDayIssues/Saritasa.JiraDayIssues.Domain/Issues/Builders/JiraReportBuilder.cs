using Saritasa.JiraDayIssues.Domain.Issues.Counters;
using Saritasa.JiraDayIssues.Domain.Issues.Dtos;
using System.Collections.Generic;
using System.Text;

namespace Saritasa.JiraDayIssues.Domain.Issues.Builders
{
    /// <summary>
    /// Static class that stores JIRA report building logic.
    /// </summary>
    public static class JiraReportBuilder
    {
        /// <summary>
        /// Method to build report as string.
        /// </summary>
        /// <returns>Report as string.</returns>
        public static string BuildStringReport(List<IssueDto> issues)
        {
            StringBuilder builder = new StringBuilder();

            string currentProjectKey = string.Empty;
            int worklogsTotalTime;

            foreach (var issue in issues)
            {
                if (issue.ProjectKey != currentProjectKey)
                {
                    builder.Append($"\n{issue.ProjectKey} {issue.ProjectName}");
                    currentProjectKey = issue.ProjectKey;
                }

                worklogsTotalTime = WorklogsTimeSpentCounter.GetTotalTime(issue);

                if (worklogsTotalTime != 0)
                {
                    builder.AppendFormat($"\n\t{issue.IssueKey,-10} {issue.Summary,-50} \t{JiraTimeBuilder.BuildStringTime(worklogsTotalTime),15}");

                    if (issue.TimeEstimateSeconds != null)
                    {
                        builder.Append($" / {JiraTimeBuilder.BuildStringTime((int)issue.TimeEstimateSeconds),-15}");
                    }
                }
            }

            builder.Append($"\n\nTOTAL: {JiraTimeBuilder.BuildStringTime(IssuesTotalTimeCounter.GetTotalTime(issues))}");

            return builder.ToString();
        }
    }
}
