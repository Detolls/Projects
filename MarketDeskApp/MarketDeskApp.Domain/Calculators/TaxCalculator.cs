namespace MarketDeskApp.Domain.TaxCalculators
{
    /// <summary>
    /// Static class that stores the logic for calculating taxes.
    /// </summary>
    public static class TaxCalculator
    {
        /// <summary>
        /// Method for calculate total sum with tax.
        /// </summary>
        /// <returns>Total sum with tax.</returns>
        public static decimal Calculate(decimal totalSum, decimal taxPercent)
        {
            return totalSum + (totalSum / 100 * taxPercent);
        }
    }
}
