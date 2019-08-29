using System;
using System.Text;

namespace Saritasa.JiraDayIssues.Domain.Issues.Builders
{
    /// <summary>
    /// A static class that stores logic for building time in JIRA format.
    /// </summary>
    public static class JiraTimeBuilder
    {
        /// <summary>
        /// Method to build time in JIRA format (1 week = 5 days, 1 day = 8 hours) as string (weeks,days,hours,minutes).
        /// </summary>
        /// <returns>Time as string.</returns>
        public static string BuildStringTime(int time)
        {
            StringBuilder builder = new StringBuilder();

            TimeSpan resultTime = TimeSpan.FromSeconds(time);
            int totalHours = (int)resultTime.TotalHours;

            int? jiraWeeks = GetJiraWeeks(totalHours);
            int? jiraDays = GetJiraDays(totalHours);
            int? jiraHours = GetJiraHours(totalHours);

            if (jiraWeeks != null)
            {
                builder.Append($"{jiraWeeks}w ");
            }

            if (jiraDays != null)
            {
                builder.Append($"{jiraDays}d ");
            }

            if (jiraHours != null)
            {
                builder.Append($"{jiraHours}h ");
            }

            if (resultTime.Minutes != 0)
            {
                builder.Append($"{resultTime.Minutes}m");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Method to get JIRA specific weeks.
        /// </summary>
        /// <returns>JIRA specific weeks.</returns>
        private static int? GetJiraWeeks(int hours)
        {
            int jiraWeeks;

            jiraWeeks = hours >= 40 ? hours / 40 : 0;

            return jiraWeeks;
        }

        /// <summary>
        /// Method to get JIRA specific days.
        /// </summary>
        /// <returns>JIRA specific days.</returns>
        private static int? GetJiraDays(int hours)
        {
            int jiraDays;

            jiraDays = hours >= 40 ? (hours % 40) / 8 : hours >= 8 ? hours / 8 : 0;

            return jiraDays;
        }

        /// <summary>
        /// Method to get JIRA specific hours.
        /// </summary>
        /// <returns>JIRA specific hours.</returns>
        private static int? GetJiraHours(int hours)
        {
            int jiraHours;

            jiraHours = hours % 8 != 0 ? hours % 8 : 0;

            return jiraHours;
        }
    }
}
