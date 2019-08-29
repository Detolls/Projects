using Saritasa.Tools.Domain.Exceptions;

namespace CurrencyReaderApp.Model.Exceptions.Domain
{
    /// <summary>
    /// Class that stores exception handlers for incorrect date format.
    /// </summary>
    public class DateFormatException : DomainException
    {
        /// <summary>
        /// Excpetion handler for incorrect date format.
        /// </summary>
        public DateFormatException(string message) : base(message)
        {
        }
    }
}
