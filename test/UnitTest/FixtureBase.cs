using System.Globalization;
using System.Threading;
using IndependentReserve.DotNetClientApi;
using NUnit.Framework;

namespace UnitTest
{
    /// <summary>
    ///     The test fixture base class.
    /// </summary>
    [TestFixture]
    public abstract class FixtureBase
    {
        protected const string ApiKey = "3de4047d-81e4-421e-8402-9dddb8971b32";
        protected const string ApiSecret = "6195d2ef6c0e4c6d94bb0c8a982ef825";
        protected const string BaseUrl = "http://api.ir.localhost";

        /// <summary>
        ///     The test fixture setup method.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            //  set culture to en-US
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }
    }

    [Category("Private")]
    public class PrivateFixtureBase : FixtureBase
    {

        protected Client CreatePrivateClient()
        {
            return Client.CreatePrivate(ApiKey, ApiSecret, BaseUrl);
        }

    }

    [Category("Public")]
    public class PublicFixtureBase : FixtureBase
    {

        protected Client CreatePublicClient()
        {
            return Client.CreatePublic(BaseUrl);
        }
    }
}
