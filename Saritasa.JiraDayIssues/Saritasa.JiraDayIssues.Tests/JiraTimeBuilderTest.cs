using Saritasa.JiraDayIssues.Domain.Issues.Builders;
using Xunit;

namespace Saritasa.JiraDayIssues.Tests
{
    /// <summary>
    /// Class for testring <seealso cref="JiraTimeBuilder"/> static class.
    /// </summary>
    public class JiraTimeBuilderTest
    {
        [Theory]
        [InlineData(39000, "1d 2h 50m")]
        [InlineData(45790, "1d 4h 43m")]
        [InlineData(10000, "2h 46m")]
        public void BuildStringTime_TimeInSeconds_ReturnsTimeAsStringInJiraFormat(int time, string excepted)
        {
            // Act.
            string result = JiraTimeBuilder.BuildStringTime(time);

            // Assert.
            Assert.Equal(excepted, result);
        }
    }
}
