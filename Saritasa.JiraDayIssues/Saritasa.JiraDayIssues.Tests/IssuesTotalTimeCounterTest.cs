using Saritasa.JiraDayIssues.Domain.Issues.Counters;
using Saritasa.JiraDayIssues.Domain.Issues.Dtos;
using System.Collections.Generic;
using Xunit;

namespace Saritasa.JiraDayIssues.Tests
{
    /// <summary>
    /// Class for testring <seealso cref="IssuesTotalTimeCounter"/> static class.
    /// </summary>
    public class IssuesTotalTimeCounterTest
    {
        [Fact]
        public void GetIssuesTotalTime_Issues_ReturnsTotalTimeInSeconds()
        {
            // Arrange.
            List<IssueDto> issues = new List<IssueDto>();

            IssueDto issueOne = new IssueDto();
            issueOne.Worklogs.Add(new WorklogDto { TimeSpentSeconds = 300 });
            issueOne.Worklogs.Add(new WorklogDto { TimeSpentSeconds = 450 });
            issueOne.Worklogs.Add(new WorklogDto { TimeSpentSeconds = 1010 });

            IssueDto issueTwo = new IssueDto();
            issueTwo.Worklogs.Add(new WorklogDto { TimeSpentSeconds = 10 });
            issueTwo.Worklogs.Add(new WorklogDto { TimeSpentSeconds = 399 });
            issueTwo.Worklogs.Add(new WorklogDto { TimeSpentSeconds = 7079 });

            issues.Add(issueOne);
            issues.Add(issueTwo);

            int excepted = 0;

            foreach (var issue in issues)
            {
                excepted += WorklogsTimeSpentCounter.GetTotalTime(issue);
            }

            // Act.
            int result = IssuesTotalTimeCounter.GetTotalTime(issues);

            // Assert.
            Assert.Equal(excepted, result);
        }
    }
}
