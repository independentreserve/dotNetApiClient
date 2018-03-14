using IndependentReserve.DotNetClientApi.Data;
using NUnit.Framework;

namespace UnitTest
{
    partial class ClientFixture
    {
		[Test]
        public void GetRecentTrades()
        {
            using (var client = CreatePublicClient())
            {
                var recentTrades = client.GetRecentTrades(CurrencyCode.Xbt, CurrencyCode.Usd, 24);
				Assert.IsNotNull(recentTrades);
            }
        }
    }
}
