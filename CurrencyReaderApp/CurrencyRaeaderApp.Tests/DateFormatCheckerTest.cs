using CurrencyReaderApp.Domain.Checkers;
using Xunit;

namespace CurrencyRaeaderApp.Tests
{
    /// <summary>
    /// Class for testing <seealso cref="DateFormatChecker"/> class.
    /// </summary>
    public class DateFormatCheckerTest
    {
        [Theory]
        [InlineData("2018-01-01")]
        [InlineData("2019-03-15")]
        [InlineData("1990-11-12")]
        public void Check_CorrectDateFormat_ReturnsTrue(string date)
        {
            // Arrange.
            bool excepted = true;

            // Act.
            bool result = DateFormatChecker.Check(date);

            // Assert.
            Assert.Equal(excepted, result);
        }
    }
}
