using System;
using Saritasa.Tools.Domain.Exceptions;

namespace CurrencyReaderApp.Model.Exceptions.Domain
{
    /// <summary>
    /// Сlass that stores exception handlers for currency not found.
    /// </summary>
    public class CurrencyNotFoundException : DomainException
    {
        /// <summary>
        /// Exception Handler for Currency Not Found.
        /// </summary>
        public CurrencyNotFoundException(DateTime date)
            : base($"{date.ToString("yyyy-MM-dd")} : [!] Currency for specific date is not found.")
        {
        }
    }
}
