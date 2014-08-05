using IndependentReserve.DotNetClientApi.Data;
using NUnit.Framework;

namespace UnitTest
{
    partial class ClientFixture
    {
		[Test]
        public void GetTradeHistorySummary()
        {
            using (var client = CreateClient())
            {
                var tradeHistory = client.GetTradeHistorySummary(CurrencyCode.Xbt, CurrencyCode.Usd, 24);

				Assert.IsNotNull(tradeHistory);
            }
        }
    }
}
