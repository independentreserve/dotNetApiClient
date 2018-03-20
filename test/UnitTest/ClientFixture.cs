using IndependentReserve.DotNetClientApi;
using NUnit.Framework;
using System;

namespace UnitTest
{
    [TestFixture]
    public partial class ClientFixture : FixtureBase
    {

        [Test]
        public void CreatePublicClientSucceed()
        {
            var client = Client.CreatePublic("https://fake.ir.domain");
            Assert.IsNotNull(client);
        }

    }
}
