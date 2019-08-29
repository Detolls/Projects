using CurrencyReaderApp.Domain.Data.Json;
using CurrencyReaderApp.Domain.Interfaces;
using CurrencyReaderApp.Model.Exceptions.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;

namespace CurrencyReaderApp.Domain.DataReaders
{
    /// <summary>
    /// A class that contains logic to implement loading a file from a remote data source to a local source.
    /// </summary>
    public class CurrencyReader : ICurrencyReader
    {
        /// <summary>
        /// From where the file will be downloaded.
        /// </summary>
        private const string Url = "https://dotnetwiki.saritasa.io/camp/data/rates.json";

        private static List<CurrencyJsonData> jsonItems;
        private static CultureInfo culture = new CultureInfo("en-US");

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyReader"/> class.
        /// </summary>
        public CurrencyReader()
        {
            string data = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        data = sr.ReadToEnd();
                    }
                }
            }

            jsonItems = JsonConvert.DeserializeObject<List<CurrencyJsonData>>(data.ToString());
        }

        /// <summary>
        /// Read currency from data source.
        /// </summary>
        /// <returns>Currency.</returns>
        public decimal GetCurrencyByDate(DateTime date)
        {
            decimal? currency = null;

            foreach (var item in jsonItems)
            {
                if (date == item.Date)
                {
                    currency = System.Threading.Thread.CurrentThread.CurrentCulture == CultureInfo.GetCultureInfo("ru-RU")
                                ? item.Rub
                                : item.Eur;
                    break;
                }
            }

            if (currency == null)
            {
                throw new CurrencyNotFoundException(date);
            }
            else
            {
                return (decimal)currency;
            }
        }
    }
}
