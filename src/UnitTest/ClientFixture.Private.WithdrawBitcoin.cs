using System;
using System.Linq;
using IndependentReserve.DotNetClientApi.Data;
using NUnit.Framework;

namespace UnitTest
{
    partial class ClientFixture
    {
        [Test]
        public void WithdrawBitcoin()
        {
            using (var client = CreatePrivateClient())
            {
                var date = DateTime.UtcNow;

                var account = client.GetAccounts().First(a => a.CurrencyCode == CurrencyCode.Xbt);

                var transactions = client.GetTransactions(account.AccountGuid, date, null, null, 1, 10);

                Assert.AreEqual(0, transactions.Data.Count());

                var bitcoinDepositAddress = client.GetBitcoinDepositAddress();

                Assert.IsNotNull(bitcoinDepositAddress);

                client.WithdrawBitcoin(0.01m, bitcoinDepositAddress.BitcoinAddress);

                transactions = client.GetTransactions(account.AccountGuid, date, null, null, 1, 10);

                var data = transactions.Data.ToList();

                Assert.Greater(data.Count, 0);
                Assert.IsTrue(data.Exists(t => t.Type == TransactionType.Withdrawal));
                Assert.IsTrue(data.Exists(t => t.Type == TransactionType.WithdrawalFee));
            }
        }
    }
}