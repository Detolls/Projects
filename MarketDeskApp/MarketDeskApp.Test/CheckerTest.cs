using MarketDeskApp.Domain.Checkers;
using Xunit;

namespace MarketDeskApp.Test
{
    /// <summary>
    /// Class that stores unit tests for class Checker.
    /// </summary>
    public class CheckerTest
    {
        [Theory]
        [InlineData("Milk,4.0,10")]
        [InlineData("Apple,5.9,20")]
        [InlineData("Chocolate,3.2,45")]
        public void CheckLine_CorrectUserLine_ReturnsTrue(string line)
        {
            // Arrange.
            bool excepted = true;

            // Act.
            bool result = Checker.CheckLine(line);

            // Assert.
            Assert.Equal(excepted, result);
        }

        [Theory]
        [InlineData("Orange 10 20")]
        [InlineData("Milk.30.40")]
        [InlineData("10, 20")]
        public void CheckLine_IncorrectUserLine_ReturnsFalse(string line)
        {
            // Arrange.
            bool excepted = false;

            // Act.
            bool result = Checker.CheckLine(line);

            // Assert.
            Assert.Equal(excepted, result);
        }
    }
}
