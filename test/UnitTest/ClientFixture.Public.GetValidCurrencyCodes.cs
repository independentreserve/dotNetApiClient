using System.Collections.Generic;
using System.Linq;
using IndependentReserve.DotNetClientApi.Data;
using NUnit.Framework;

namespace UnitTest
{
    partial class ClientFixture
    {
        [Test]
        public void GetValidPrimaryCurrencyCodes()
        {
            using (var client = CreatePublicClient())
            {
                IEnumerable<CurrencyCode> currencyCodes = client.GetValidPrimaryCurrencyCodes();

                Assert.AreEqual(currencyCodes.Count(),1);
                Assert.AreEqual(currencyCodes.First(), CurrencyCode.Xbt);
            }
        }

        [Test]
        public void GetValidSecondaryCurrencyCodes()
        {
            using (var client = CreatePublicClient())
            {
                IEnumerable<CurrencyCode> currencyCodes = client.GetValidSecondaryCurrencyCodes();

                Assert.AreEqual(currencyCodes.Count(), 2);
                Assert.AreEqual(currencyCodes.First(), CurrencyCode.Usd);
                Assert.AreEqual(currencyCodes.Last(), CurrencyCode.Aud);
            }
        }
    }
}
