using MarketDeskApp.Model.Entites;
using MarketDeskApp.Model.Entites.Base;
using MarketDeskApp.Model.Exceptions.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarketDeskApp.Model
{
    /// <summary>
    /// Order entity.
    /// </summary>
    public class Order : BaseEntity
    {
        /// <summary>
        /// List of order items.
        /// </summary>
        private readonly List<OrderItem> orderItems = new List<OrderItem>();

        private bool IsValid(OrderItem orderItem)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (Validator.TryValidateObject(orderItem, new ValidationContext(orderItem), results, true))
            {
                return true;
            }
            else
            {
                throw new OrderAddException(results);
            }
        }

        /// <summary>
        /// Get order items.
        /// </summary>
        public List<OrderItem> GetOrderItems()
        {
            return orderItems;
        }

        /// <summary>
        /// Add order item.
        /// </summary>
        public void Add(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                throw new ArgumentNullException(nameof(orderItem));
            }

            if (IsValid(orderItem))
            {
                orderItems.Add(orderItem);
            }
        }

        /// <summary>
        /// Remove order item.
        /// </summary>
        public void Remove(OrderItem orderItem)
        {
            orderItems.Remove(orderItem);
        }
    }
}
