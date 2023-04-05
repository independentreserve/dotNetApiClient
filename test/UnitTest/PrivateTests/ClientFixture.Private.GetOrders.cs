using System.Linq;
using IndependentReserve.DotNetClientApi.Data;
using NUnit.Framework;

namespace UnitTest
{
    partial class PrivateClientFixture : PrivateFixtureBase
    {
        [Test]
        public void GetOpenOrders()
        {
            using (var client = CreatePrivateClient())
            {
                var page = client.GetOpenOrders(CurrencyCode.Xbt, CurrencyCode.Usd, 1, 10);

                Assert.IsNotNull(page);
                Assert.AreEqual(page.PageSize, 10);
            }
        }

        [Test]
        public void GetOpenOrdersNoCurrencies()
        {
            using (var client = CreatePrivateClient())
            {
                Page<BankHistoryOrder> page = client.GetOpenOrders(null, null, 1, 10);

                Assert.IsNotNull(page);

                Assert.AreEqual(page.PageSize, 10);
                Assert.IsTrue(page.TotalItems > 0, $"page.TotalItems is {page.TotalItems}");

                Assert.IsTrue(page.Data.Any(), "page.Data has no items");
                Assert.IsTrue(page.Data.Count() <= 10, $"page.Data.Count() is {page.Data.Count()}");
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
                Assert.IsTrue(page.TotalItems > 0, $"page.TotalItems is {page.TotalItems}");

                Assert.IsTrue(page.Data.Any(), "page.Data has no items");
                Assert.IsTrue(page.Data.Count() <= 10, $"page.Data.Count() is {page.Data.Count()}");
            }
        }

        [Test]
        public void GetClosedOrdersNoCurrencies()
        {
            using (var client = CreatePrivateClient())
            {
                Page<BankHistoryOrder> page = client.GetClosedOrders(null, null, 1, 10);

                Assert.IsNotNull(page);

                Assert.AreEqual(page.PageSize, 10);
                Assert.IsTrue(page.TotalItems > 0, $"page.TotalItems is {page.TotalItems}");

                Assert.IsTrue(page.Data.Any(), "page.Data has no items");
                Assert.IsTrue(page.Data.Count() <= 10, $"page.Data.Count() is {page.Data.Count()}");
            }
        }

        [Test]
        public void GetClosedFilledOrders()
        {
            using (var client = CreatePrivateClient())
            {
                Page<BankHistoryOrder> page = client.GetClosedFilledOrders(CurrencyCode.Xbt, CurrencyCode.Usd, 1, 10);

                Assert.IsNotNull(page);

                Assert.AreEqual(page.PageSize, 10);
                Assert.IsTrue(page.TotalItems > 0, $"page.TotalItems is {page.TotalItems}");

                Assert.IsTrue(page.Data.Any(), "page.Data has no items");
                Assert.IsTrue(page.Data.Count() <= 10, $"page.Data.Count() is {page.Data.Count()}");
            }
        }

        /// <summary>
        /// Marked as Brittle since it sometimes breaks unit test due to timeout in Bank when executing dbo.BankOrderGetPaged without currency params
        /// </summary>
        [Category("Brittle")]
        [Test]
        public void GetClosedFilledOrdersNoCurrencies()
        {
            using (var client = CreatePrivateClient())
            {
                Page<BankHistoryOrder> page = client.GetClosedFilledOrders(null, null, 1, 10);

                Assert.IsNotNull(page);

                Assert.AreEqual(page.PageSize, 10);
                Assert.IsTrue(page.TotalItems > 0, $"page.TotalItems is {page.TotalItems}");

                Assert.IsTrue(page.Data.Any(), "page.Data has no items");
                Assert.IsTrue(page.Data.Count() <= 10, $"page.Data.Count() is {page.Data.Count()}");
            }
        }
    }
}
