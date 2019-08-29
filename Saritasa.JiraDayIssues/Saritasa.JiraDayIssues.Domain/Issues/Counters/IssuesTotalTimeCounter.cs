using Saritasa.JiraDayIssues.Domain.Issues.Dtos;
using System.Collections.Generic;

namespace Saritasa.JiraDayIssues.Domain.Issues.Counters
{
    /// <summary>
    /// Static class that stores logic for counting issues total time.
    /// </summary>
    public static class IssuesTotalTimeCounter
    {
        /// <summary>
        /// Method to get issues total time.
        /// </summary>
        public static int GetTotalTime(List<IssueDto> issues)
        {
            int totalTime = 0;

            foreach (var issue in issues)
            {
                totalTime += WorklogsTimeSpentCounter.GetTotalTime(issue);
            }

            return totalTime;
        }
    }
}
