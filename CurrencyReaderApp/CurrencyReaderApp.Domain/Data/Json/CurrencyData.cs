using System;

namespace CurrencyReaderApp.Domain.Data.Json
{
    /// <summary>
    /// Class that stores file data from a JSON file.
    /// </summary>
    public class CurrencyJsonData
    {
        public DateTime Date { get; set; }

        public decimal Rub { get; set; }

        public decimal Eur { get; set; }
    }
}
