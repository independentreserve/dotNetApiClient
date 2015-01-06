using IndependentReserve.DotNetClientApi.Data;
using NUnit.Framework;

namespace UnitTest
{
    partial class ClientFixture
    {
        [Test]
        public void PlaceLimitOrder()
        {
            using (var client = CreatePrivateClient())
            {
                BankOrder bankOrder = client.PlaceLimitOrder(CurrencyCode.Xbt, CurrencyCode.Usd, OrderType.LimitOffer, 500.01m, 1);
                Assert.IsNotNull(bankOrder);
            }
        }

        [Test]
        public void PlaceMarketOrder()
        {
            using (var client = CreatePrivateClient())
            {
                BankOrder bankOrder = client.PlaceMarketOrder(CurrencyCode.Xbt, CurrencyCode.Usd, OrderType.MarketBid, 0.01m);
                Assert.IsNotNull(bankOrder);
            }
        }

        [Test]
        public void CancelOrder()
        {
            using (var client = CreatePrivateClient())
            {
                BankOrder bankOrder = client.PlaceLimitOrder(CurrencyCode.Xbt, CurrencyCode.Usd, OrderType.LimitOffer, 500, 1);
                Assert.IsNotNull(bankOrder);

                Assert.AreEqual(bankOrder.Status, OrderStatus.Open);

                bankOrder = client.CancelOrder(bankOrder.OrderGuid);

                Assert.IsNotNull(bankOrder);

                Assert.AreEqual(bankOrder.Status,OrderStatus.Cancelled);
            }
            
        }
    }
}
