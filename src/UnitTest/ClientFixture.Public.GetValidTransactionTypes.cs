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

                Assert.AreEqual(transactionTypes.Count, 2);
                Assert.IsTrue(transactionTypes.Contains(TransactionType.BitcoinNetworkFee));
                Assert.IsTrue(transactionTypes.Contains(TransactionType.Brokerage));
                Assert.IsTrue(transactionTypes.Contains(TransactionType.Commission));
                Assert.IsTrue(transactionTypes.Contains(TransactionType.Deposit));
                Assert.IsTrue(transactionTypes.Contains(TransactionType.DepositFee));
                Assert.IsTrue(transactionTypes.Contains(TransactionType.Error));
                Assert.IsTrue(transactionTypes.Contains(TransactionType.GST));
                Assert.IsTrue(transactionTypes.Contains(TransactionType.Trade));
                Assert.IsTrue(transactionTypes.Contains(TransactionType.Transfer));
                Assert.IsTrue(transactionTypes.Contains(TransactionType.Unclaimed));
                Assert.IsTrue(transactionTypes.Contains(TransactionType.Withdrawal));
                Assert.IsTrue(transactionTypes.Contains(TransactionType.WithdrawalFee));
            }
        }
    }
}
