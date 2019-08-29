using Saritasa.JiraDayIssues.Domain.Issues.Dtos;

namespace Saritasa.JiraDayIssues.Domain.Issues.Counters
{
    /// <summary>
    /// Static class that stores logic for counting workolgs total time spent.
    /// </summary>
    public static class WorklogsTimeSpentCounter
    {
        /// <summary>
        /// Method to get worklogs total time spent.
        /// </summary>
        public static int GetTotalTime(IssueDto issue)
        {
            int totalTime = 0;

            foreach (var worklog in issue.Worklogs)
            {
                totalTime += worklog.TimeSpentSeconds;
            }

            return totalTime;
        }
    }
}
