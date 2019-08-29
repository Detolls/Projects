using CurrencyReaderApp.Model.Exceptions.Domain;
using System;
using System.Globalization;

namespace CurrencyReaderApp.Domain.Checkers
{
    /// <summary>
    /// Сlass that stores the logic for checking the date format.
    /// </summary>
    public static class DateFormatChecker
    {
        /// <summary>
        /// Date format.
        /// </summary>
        private const string DateFormat = "yyyy-MM-dd";

        /// <summary>
        /// Method to check the date format.
        /// </summary>
        /// <param name="date">Input date.</param>
        public static bool Check(string date)
        {
            DateTime result;
            if (!DateTime.TryParseExact(date, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                throw new DateFormatException("Invalid date format.");
            }
            else
            {
                return true;
            }
        }
    }
}
