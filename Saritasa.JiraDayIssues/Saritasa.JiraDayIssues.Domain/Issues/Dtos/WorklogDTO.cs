namespace Saritasa.JiraDayIssues.Domain.Issues.Dtos
{
    /// <summary>
    /// Worklog Data Transfer Object.
    /// </summary>
    public class WorklogDto
    {
        /// <summary>
        /// Worklog comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Worklog time spent in seconds.
        /// </summary>
        public int TimeSpentSeconds { get; set; }
    }
}
