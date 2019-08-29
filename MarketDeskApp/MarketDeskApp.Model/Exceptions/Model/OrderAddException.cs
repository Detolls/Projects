using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MarketDeskApp.Model.Exceptions.Model
{
    /// <summary>
    /// Class that stores exception handlers for order entity.
    /// </summary>
    public class OrderAddException : Exception
    {
        /// <summary>
        /// Exception handling when trying to add order item.
        /// </summary>
        public OrderAddException(List<ValidationResult> results) : base(GetModelErrors(results))
        {
        }

        /// <summary>
        /// A method for getting all model errors in the form of a string.
        /// </summary>
        private static string GetModelErrors(List<ValidationResult> results)
        {
            StringBuilder errors = new StringBuilder();

            foreach (var error in results)
            {
                errors.Append(error.ErrorMessage).Append("\n");
            }

            return errors.ToString();
        }
    }
}
