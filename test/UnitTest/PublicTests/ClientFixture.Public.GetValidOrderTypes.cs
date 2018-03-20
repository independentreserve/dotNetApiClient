using System.Collections.Generic;
using System.Linq;
using IndependentReserve.DotNetClientApi.Data;
using NUnit.Framework;

namespace UnitTest
{
    partial class PublicClientFixture
    {
        [Test]
        public void GetValidLimitOrderTypes()
        {
            using (var client = CreatePublicClient())
            {
                IEnumerable<OrderType> orderTypes = client.GetValidLimitOrderTypes();

                Assert.AreEqual(orderTypes.Count(), 2);
                Assert.IsTrue(orderTypes.Contains(OrderType.LimitBid));
                Assert.IsTrue(orderTypes.Contains(OrderType.LimitOffer));
            }
        }

        [Test]
        public void GetValidMarketOrderTypes()
        {
            using (var client = CreatePublicClient())
            {
                IEnumerable<OrderType> orderTypes = client.GetValidMarketOrderTypes();

                Assert.AreEqual(orderTypes.Count(), 2);
                Assert.IsTrue(orderTypes.Contains(OrderType.MarketBid));
                Assert.IsTrue(orderTypes.Contains(OrderType.MarketOffer));
            }
        }
    }
}
