using CurrencyReaderApp.Domain.CacheReaders;
using CurrencyReaderApp.Domain.DataReaders;
using System;
using Xunit;

namespace CurrencyRaeaderApp.Tests
{
    /// <summary>
    /// Class for testring <seealso cref="CacheCurrencyReader"/> class.
    /// </summary>
    public class CurrencyCacheReaderTest
    {
        [Fact]
        public void Read_InputDate_CorrectCurrencyExcepted()
        {
            // Arrange.
            DateTime date1 = DateTime.Parse("2018-08-01");
            DateTime date2 = DateTime.Parse("2018-07-31");
            DateTime date3 = DateTime.Parse("2018-07-14");

            decimal excepted1 = 0.852m;
            decimal excepted2 = 0.852m;
            decimal excepted3 = 0.859m;

            CurrencyReader reader = new CurrencyReader();
            CacheCurrencyReader cacheReader = new CacheCurrencyReader(reader);

            // Act.
            decimal result1 = cacheReader.GetCurrencyByDate(date1);
            decimal result2 = cacheReader.GetCurrencyByDate(date2);
            decimal result3 = cacheReader.GetCurrencyByDate(date3);

            // Assert.
            Assert.Equal(excepted1, result1);
            Assert.Equal(excepted2, result2);
            Assert.Equal(excepted3, result3);
        }
    }
}
