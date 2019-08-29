using MarketDeskApp.Domain.Calculators;
using MarketDeskApp.Domain.Checkers;
using MarketDeskApp.Domain.Parsers;
using MarketDeskApp.Domain.TaxCalculators;
using MarketDeskApp.Model;
using MarketDeskApp.Model.Entites;
using MarketDeskApp.Model.Exceptions.Model;
using System;
using System.Globalization;
using System.Threading;

namespace MarketDeskApp
{
    /// <summary>
    /// Program class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        static void Main(string[] args)
        {
            // Create order.
            Order order = new Order();

            // Order item.
            OrderItem orderItem;

            // User input.
            string input;

            // Mathes pattern user input.
            bool isMathesPattern;

            Console.WriteLine("Enter line by line the list of products that the customer ordered: (Input template: \"Milk,10.0,4\")\n" +
                              "If you need to complete, make an empty entry.\n");

            while (true)
            {
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                // Check user input.
                isMathesPattern = Checker.CheckLine(input);

                if (isMathesPattern == false)
                {
                    Console.WriteLine("Input does not match the pattern.");
                    continue;
                }

                // Create order item.
                orderItem = OrderItemParser.Parse(input);

                try
                {
                    // Add order item to order.
                    order.Add(orderItem);
                }
                catch (OrderAddException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // Total sum of order.
            decimal totalSum = TotalSumCalculator.Calculate(order);

            // Total sum with tax of order.
            decimal totalSumWithTax = TaxCalculator.Calculate(totalSum, 3);

            // Use a certain culture
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Console.WriteLine($"Total: {totalSum:C}");
            Console.WriteLine($"Total with tax: {totalSumWithTax:C}");

            // Delay.
            Console.ReadKey();
        }
    }
}
