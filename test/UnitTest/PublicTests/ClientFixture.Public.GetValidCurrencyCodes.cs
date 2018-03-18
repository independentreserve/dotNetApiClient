using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndependentReserve.DotNetClientApi;
using IndependentReserve.DotNetClientApi.Data;
using Moq;
using NUnit.Framework;

namespace UnitTest
{
    partial class PublicClientFixture
    {
        /// <summary>
        /// https://github.com/independentreserve/dotNetApiClient/issues/5
        /// </summary>
        [Test]
        public void GetValidPrimaryCurrencyCodes_DoesNotBreakOnUnexpectedCoins()
        {
            var mockApiOutput = new[] { "Xbt","Eth", "NewCodeFromServer" };
            var mockHttpWorker = new Mock<IHttpWorker>();

            mockHttpWorker.Setup(w => w.QueryPublicAsync<string[]>(It.IsAny<string>()))
                        .Returns(Task.FromResult(mockApiOutput));

            using (var client = CreatePublicClient())
            {
                client.HttpWorker = mockHttpWorker.Object;
                var currencyCodes = client.GetValidPrimaryCurrencyCodes().ToList();

                Assert.AreEqual(2, currencyCodes.Count);
                Assert.Contains(CurrencyCode.Xbt, currencyCodes);
            }
        }

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
