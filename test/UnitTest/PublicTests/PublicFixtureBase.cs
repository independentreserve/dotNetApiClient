using IndependentReserve.DotNetClientApi;
using NUnit.Framework;

namespace UnitTest
{
    [Category("Public")]
    public class PublicFixtureBase : FixtureBase
    {

        protected Client CreatePublicClient()
        {
            return Client.CreatePublic(GetConfig().BaseUrl);
        }
    }
}
