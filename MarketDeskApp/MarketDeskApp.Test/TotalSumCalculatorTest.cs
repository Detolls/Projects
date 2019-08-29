using MarketDeskApp.Domain.Calculators;
using MarketDeskApp.Model;
using MarketDeskApp.Model.Entites;
using Xunit;

namespace MarketDeskApp.Test
{
    /// <summary>
    /// Class that stores unit tests for class TotalSumCalculator.
    /// </summary>
    public class TotalSumCalculatorTest
    {
        [Fact]
        public void Calculate_Order_TotalSum()
        {
            // Arrange.
            Order order = new Order();

            order.Add(new OrderItem { Name = "Milk", Price = 10.0M, Quantity = 10 });
            order.Add(new OrderItem { Name = "Chocolate", Price = 4.5M, Quantity = 20 });
            order.Add(new OrderItem { Name = "Coffee", Price = 1M, Quantity = 345 });

            decimal excepted = 0;

            foreach (var orderItem in order.GetOrderItems())
            {
                excepted += orderItem.Price * orderItem.Quantity;
            }

            // Act.
            decimal result = TotalSumCalculator.Calculate(order);

            // Assert.
            Assert.Equal(excepted, result);
        }
    }
}
