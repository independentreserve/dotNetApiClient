using IndependentReserve.DotNetClientApi;
using NUnit.Framework;

namespace UnitTest
{
    [Category("Private")]
    public class PrivateFixtureBase : FixtureBase
    {

        protected Client CreatePrivateClient()
        {
            return Client.Create(GetConfig());
        }

    }
}
