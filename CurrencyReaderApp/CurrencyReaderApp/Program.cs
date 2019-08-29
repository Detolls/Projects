using CurrencyReaderApp.Domain.CacheReaders;
using CurrencyReaderApp.Domain.Checkers;
using CurrencyReaderApp.Domain.DataReaders;
using CurrencyReaderApp.Model.Exceptions.Domain;
using System;
using System.Globalization;

namespace CurrencyReaderApp
{
    /// <summary>
    /// Program class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// App entry point.
        /// </summary>
        static void Main(string[] args)
        {
            CurrencyReader reader = new CurrencyReader();
            CacheCurrencyReader cacheReader = new CacheCurrencyReader(reader);

            CultureInfo culture = new CultureInfo("en-US");

            Console.WriteLine("Enter date or dates (with comma) in ISO format (yyyy-MM-dd). " +
                              "\nFor example: \"2018-01-01\" or \"2018-01-01,2018-02-02\":" +
                              "Enter a blank line to exit.\n");

            // User input.
            string input;

            // Input dates.
            string[] dates;

            // Currency name.
            string currencyName = System.Threading.Thread.CurrentThread.CurrentCulture == CultureInfo.GetCultureInfo("ru-RU")
                                  ? "RUB"
                                  : "EUR";

            while (true)
            {
                input = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(input))
                {
                    break;
                }
                else
                {
                    dates = input.Split(',');
                }

                foreach (var date in dates)
                {
                    try
                    {
                        DateFormatChecker.Check(date);

                        Console.WriteLine($"{date}: {cacheReader.GetCurrencyByDate(DateTime.Parse(date)).ToString(culture)} {currencyName}");
                    }
                    catch (DateFormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (CurrencyNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
