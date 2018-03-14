using System.Collections.Generic;
using System.Linq;
using IndependentReserve.DotNetClientApi.Data;
using NUnit.Framework;

namespace UnitTest
{
    partial class ClientFixture
    {
        [Test]
        public void GetValidTransactionTypes()
        {
            using (var client = CreatePublicClient())
            {
                var transactionTypes = client.GetValidTransactionTypes().ToList();

                Assert.GreaterOrEqual(transactionTypes.Count, 2);
            }
        }
    }
}
