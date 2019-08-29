using MarketDeskApp.Model.Entites;
using MarketDeskApp.Model.Exceptions.Domain;
using System.Globalization;

namespace MarketDeskApp.Domain.Parsers
{
    /// <summary>
    /// Static class that stores logic for parsing to the order item.
    /// </summary>
    public static class OrderItemParser
    {
        private static readonly CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-US");

        /// <summary>
        /// Method for parsing user input to the order item.
        /// </summary>
        /// <param name="input">User input.</param>
        /// <returns></returns>
        public static OrderItem Parse(string input)
        {
            // Get input fields.
            string[] fields = input.Split(',');

            string nameOrderItem = fields[0];

            if (!decimal.TryParse(fields[1], NumberStyles.Float, cultureInfo, out decimal priceOrderItem))
            {
                throw new OrderItemParseException("Can't parse price value.");
            }

            if (!int.TryParse(fields[2], out int quantityOrderItem))
            {
                throw new OrderItemParseException("Can't parse quantity value.");
            }

            return new OrderItem
            {
                Name = nameOrderItem,
                Price = priceOrderItem,
                Quantity = quantityOrderItem
            };
        }
    }
}
