using IndependentReserve.DotNetClientApi.Data;
using NUnit.Framework;

namespace UnitTest
{
    partial class PrivateClientFixture
    {
        [Test]
        public void PlaceLimitOrder()
        {
            using (var client = CreatePrivateClient())
            {
                BankOrder bankOrder = client.PlaceLimitOrder(CurrencyCode.Xbt, CurrencyCode.Usd, OrderType.LimitOffer, 500.01m, 1, null);
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
        
        [Category("Brittle")]
        [Test]
        public void CancelOrder()
        {
            using (var client = CreatePrivateClient())
            {
                BankOrder bankOrder = client.PlaceLimitOrder(CurrencyCode.Xbt, CurrencyCode.Usd, OrderType.LimitOffer, 50000, 1, null);
                Assert.IsNotNull(bankOrder);

                Assert.AreEqual(bankOrder.Status, OrderStatus.Open);

                bankOrder = client.CancelOrder(bankOrder.OrderGuid);

                Assert.IsNotNull(bankOrder);

                Assert.AreEqual(bankOrder.Status,OrderStatus.Cancelled);
            }
            
        }
    }
}
