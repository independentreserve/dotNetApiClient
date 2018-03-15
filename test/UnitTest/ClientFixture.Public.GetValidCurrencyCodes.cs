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
                var currencyCodes = client.GetValidPrimaryCurrencyCodes().ToList();

                Assert.Greater(currencyCodes.Count(), 2);
                Assert.Contains(CurrencyCode.Xbt, currencyCodes);
            }
        }

        private void AssertCurrencies(List<CurrencyCode> currencyCodes)
        {
        }

        [Test]
        public void GetValidSecondaryCurrencyCodes()
        {
            using (var client = CreatePublicClient())
            {
                var currencyCodes = client.GetValidSecondaryCurrencyCodes().ToList();

                Assert.Greater(currencyCodes.Count(), 2);
                Assert.Contains(CurrencyCode.Aud, currencyCodes);
            }
        }
    }
}
