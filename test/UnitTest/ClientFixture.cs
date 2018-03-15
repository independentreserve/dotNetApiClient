using IndependentReserve.DotNetClientApi;
using NUnit.Framework;
using System;

namespace UnitTest
{
    [TestFixture]
    public partial class ClientFixture : FixtureBase
    {
        private const string ApiKey = "3de4047d-81e4-421e-8402-9dddb8971b32";
        private const string ApiSecret = "6195d2ef6c0e4c6d94bb0c8a982ef825";
        private const string BaseUrl = "http://api.ir.localhost";

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

        [Test]
        public void HMACSHA256Hash()
        {
            var output = Client.HMACSHA256Hash("message", "key");
            Assert.AreEqual("6E9EF29B75FFFC5B7ABAE527D58FDADB2FE42E7219011976917343065F58ED4A", output);
        }

        private Client CreatePrivateClient()
        {
            return Client.CreatePrivate(ApiKey, ApiSecret, BaseUrl);
        }

        private Client CreatePublicClient()
        {
            return Client.CreatePublic(BaseUrl);
        }
    }
}
