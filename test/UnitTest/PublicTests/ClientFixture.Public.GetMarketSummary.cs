using IndependentReserve.DotNetClientApi.Data;
using NUnit.Framework;

namespace UnitTest
{
    partial class PublicClientFixture : PublicFixtureBase
    {
        [Test]
        public void GetMarketSummary()
        {
            using (var client = CreatePublicClient())
            {
                var marketSummary = client.GetMarketSummary(CurrencyCode.Xbt, CurrencyCode.Usd);

                Assert.IsNotNull(marketSummary);
            }
        }
    }
}
