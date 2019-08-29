using Saritasa.JiraDayIssues.Domain.Issues.Counters;
using Saritasa.JiraDayIssues.Domain.Issues.Dtos;
using Xunit;

namespace Saritasa.JiraDayIssues.Tests
{
    /// <summary>
    /// Class for testring <seealso cref="WorklogsTimeSpentCounter"/> static class.
    /// </summary>
    public class WorklogsTimeSpentCounterTest
    {
        [Fact]
        public void GetWorklogsTotalTimeSpent_Issue_ReturnsTotalTimeInSeconds()
        {
            // Arrange.
            IssueDto issue = new IssueDto();
            issue.Worklogs.Add(new WorklogDto { TimeSpentSeconds = 301 });
            issue.Worklogs.Add(new WorklogDto { TimeSpentSeconds = 4505 });
            issue.Worklogs.Add(new WorklogDto { TimeSpentSeconds = 1999 });

            int excepted = 0;

            foreach (var worklog in issue.Worklogs)
            {
                excepted += worklog.TimeSpentSeconds;
            }

            // Act.
            int result = WorklogsTimeSpentCounter.GetTotalTime(issue);

            // Assert.
            Assert.Equal(excepted, result);
        }
    }
}
