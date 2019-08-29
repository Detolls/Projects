using MarketDeskApp.Domain.TaxCalculators;
using Xunit;

namespace MarketDeskApp.Test
{
    /// <summary>
    /// Class that stores unit tests for class TaxCalculator.
    /// </summary>
    public class TaxCalculatorTest
    {
        [Theory]
        [InlineData(300, 3.5)]
        [InlineData(200, 2.0)]
        [InlineData(500, 5)]
        public void Calculate_TotalSumAndTaxPercent_TotalSumWithTax(decimal totalSum, decimal taxPercent)
        {
            // Arrange.
            decimal excepted = totalSum + (totalSum / 100 * taxPercent);

            // Act.
            decimal result = TaxCalculator.Calculate(totalSum, taxPercent);

            // Assert.
            Assert.Equal(excepted, result);
        }
    }
}
