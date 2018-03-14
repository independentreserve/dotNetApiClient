﻿using System.Collections.Generic;
using System.Linq;
using IndependentReserve.DotNetClientApi.Data;
using NUnit.Framework;

namespace UnitTest
{
    partial class ClientFixture
    {
        [Test]
        public void GetAccounts()
        {
            using (var client = CreatePrivateClient())
            {
                IEnumerable<Account> accounts = client.GetAccounts();

                Assert.IsNotNull(accounts);

                Assert.AreEqual(accounts.Count(),2);

                Account usdAccount = accounts.FirstOrDefault(a => a.CurrencyCode == CurrencyCode.Usd);
                Assert.IsNotNull(usdAccount);

                Account xbtAccount = accounts.FirstOrDefault(a => a.CurrencyCode == CurrencyCode.Xbt);
                Assert.IsNotNull(xbtAccount);

            }
        }
    }
}
