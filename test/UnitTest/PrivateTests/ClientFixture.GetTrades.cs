using System.Linq;
using NUnit.Framework;

namespace UnitTest
{
    partial class PrivateClientFixture
    {
        [Test]
        public void GetTrades()
        {
            using (var client = CreatePrivateClient())
            {
                var trades = client.GetTrades(1, 50, null, null, true);
                Assert.IsNotNull(trades);
                Assert.Greater(trades.Data.Count(), 0);
            }
        }
    }
}