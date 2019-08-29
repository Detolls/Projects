using MarketDeskApp.Model;

namespace MarketDeskApp.Domain.Calculators
{
    /// <summary>
    /// Static class that stores the logic for calculating total sum.
    /// </summary>
    public static class TotalSumCalculator
    {
        /// <summary>
        /// Method for calculate total sum.
        /// </summary>
        public static decimal Calculate(Order order)
        {
            decimal totalSum = 0M;

            foreach (var orderItem in order.GetOrderItems())
            {
                totalSum += orderItem.Price * orderItem.Quantity;
            }

            return totalSum;
        }
    }
}
