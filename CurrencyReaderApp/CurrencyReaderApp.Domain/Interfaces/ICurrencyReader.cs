using System;

namespace CurrencyReaderApp.Domain.Interfaces
{
    /// <summary>
    /// Interfaces for reading currency.
    /// </summary>
    public interface ICurrencyReader
    {
        /// <summary>
        /// Get currency by date.
        /// </summary>
        /// <returns>Currency.</returns>
        decimal GetCurrencyByDate(DateTime date);
    }
}
