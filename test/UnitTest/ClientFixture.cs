using IndependentReserve.DotNetClientApi;
using NUnit.Framework;
using System;

namespace UnitTest
{
    [TestFixture]
    public partial class ClientFixture : FixtureBase
    {

        [Test]
        public void CreatePrivateClientSucceed()
        {
            var client = Client.CreatePrivate(ApiKey, ApiSecret, BaseUrl);

            Assert.IsNotNull(client);
        }

        [Test]
        public void CreatePublicClientSucceed()
        {
            var client = Client.CreatePublic(BaseUrl);

            Assert.IsNotNull(client);
        }

    }
}
