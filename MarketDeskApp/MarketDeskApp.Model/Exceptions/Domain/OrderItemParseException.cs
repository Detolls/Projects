using System;

namespace MarketDeskApp.Model.Exceptions.Domain
{
    /// <summary>
    /// Class that stores exception handlers for order item parser.
    /// </summary>
    public class OrderItemParseException : Exception
    {
        /// <summary>
        /// Exception handling when trying to parse an order item.
        /// </summary>
        public OrderItemParseException(string message) : base(message)
        {
        }
    }
}
