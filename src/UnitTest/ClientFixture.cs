using IndependentReserve.DotNetClientApi;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public partial class ClientFixture : FixtureBase
    {
        private const string ApiKey = "3de4047d-81e4-421e-8402-9dddb8971b32";
        private const string ApiSecret = "6195d2ef6c0e4c6d94bb0c8a982ef825";
        private const string BaseUrl = "http://api.ir.localhost";

        [Test]
        public void CreateClientSucceed()
        {
            var client = Client.CreatePrivate(ApiKey, ApiSecret, BaseUrl);

            Assert.IsNotNull(client);
        }

        private Client CreateClient()
        {
            return Client.CreatePrivate(ApiKey, ApiSecret, BaseUrl);
        }
    }
}
