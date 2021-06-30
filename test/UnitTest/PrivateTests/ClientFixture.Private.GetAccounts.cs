using System;
using System.Collections.Generic;
using System.Linq;
using IndependentReserve.DotNetClientApi.Data;
using NUnit.Framework;

namespace UnitTest
{
    partial class PrivateClientFixture
    {
        [Test]
        public void GetAccounts()
        {
            try
            {
                using (var client = CreatePrivateClient())
                {
                    IEnumerable<Account> accounts = client.GetAccounts();

                    Assert.IsNotNull(accounts);

                    Assert.That(accounts.ToList().Count > 2);

                    Account usdAccount = accounts.FirstOrDefault(a => a.CurrencyCode == CurrencyCode.Usd);
                    Assert.IsNotNull(usdAccount);

                    Account xbtAccount = accounts.FirstOrDefault(a => a.CurrencyCode == CurrencyCode.Xbt);
                    Assert.IsNotNull(xbtAccount);

                }
            }
            catch(Exception e)
            {
                var config = GetConfig();
                Assert.Fail($"Failed to GetAccounts using BaseUrl={config.BaseUrl}, {config.Credential.Key}\r\n{e}");
            }
        }
    }
}
