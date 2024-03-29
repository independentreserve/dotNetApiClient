﻿using System;
using System.Collections.Generic;
using System.Linq;
using IndependentReserve.DotNetClientApi.Data;
using NUnit.Framework;

namespace UnitTest
{
    partial class PrivateClientFixture
    {
        [Test]
        public void GetTransactions()
        {
            using (var client = CreatePrivateClient())
            {
                IEnumerable<Account> accounts = client.GetAccounts();

                Assert.IsNotNull(accounts);
                Assert.IsNotEmpty(accounts);

                Account usdAccount = accounts.FirstOrDefault(a => a.CurrencyCode == CurrencyCode.Usd);

                Assert.IsNotNull(usdAccount);

                Page<Transaction> transactions = client.GetTransactions(usdAccount.AccountGuid, new DateTime(2014, 7, 1),new DateTime(2014, 8, 1), null, 1, 25, true);

                Assert.IsNotNull(transactions);

                Assert.AreEqual(transactions.PageSize,25);

                // Any further assertions is dependent on the api key used
            }
        }
    }
}
