using System.Linq;
using IndependentReserve.DotNetClientApi.Data;
using NUnit.Framework;

namespace UnitTest
{
    partial class ClientFixture
    {
        [Test]
        public void GetOpenOrders()
        {
            using (var client = CreatePrivateClient())
            {
                Page<BankHistoryOrder> page = client.GetOpenOrders(CurrencyCode.Xbt, CurrencyCode.Usd, 1, 10);

                Assert.IsNotNull(page);

                Assert.AreEqual(page.PageSize,10);
                Assert.IsTrue(page.TotalItems>0);

                Assert.IsTrue(page.Data.Any());
                Assert.IsTrue(page.Data.Count() <= 10);
            }
        }

        [Test]
        public void GetClosedOrders()
        {
            using (var client = CreatePrivateClient())
            {
                Page<BankHistoryOrder> page = client.GetClosedOrders(CurrencyCode.Xbt, CurrencyCode.Usd, 1, 10);

                Assert.IsNotNull(page);

                Assert.AreEqual(page.PageSize, 10);
                Assert.IsTrue(page.TotalItems > 0);

                Assert.IsTrue(page.Data.Any());
                Assert.IsTrue(page.Data.Count() <= 10);
            }
        }
    }
}
