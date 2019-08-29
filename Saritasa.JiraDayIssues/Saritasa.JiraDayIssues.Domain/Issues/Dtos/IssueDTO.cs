using System.Collections.Generic;

namespace Saritasa.JiraDayIssues.Domain.Issues.Dtos
{
    /// <summary>
    /// Issue Data Transfer object.
    /// </summary>
    public class IssueDto
    {
        /// <summary>
        /// Project key.
        /// </summary>
        public string ProjectKey { get; set; }

        /// <summary>
        /// Project name.
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Issue key.
        /// </summary>
        public string IssueKey { get; set; }

        /// <summary>
        /// Summary.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Time estimate in seconds.
        /// </summary>
        public int? TimeEstimateSeconds { get; set; }

        /// <summary>
        /// List of worklogs.
        /// </summary>
        public List<WorklogDto> Worklogs { get; set; } = new List<WorklogDto>();

        /// <summary>
        /// Method <seealso cref="ToString"/> override.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format($"{IssueKey}\n{Summary}");
        }
    }
}
