using IndependentReserve.DotNetClientApi.Helpers;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class Fnv1aFixture : FixtureBase
    {
        [TestCase("Independent Reserve", 0x95e5cd89)]
        [TestCase("", 0x811C9DC5)]
        public void Compute(string value, uint expected)
        {
            var actual = Fnv1a.ComputeHash(value);

            Assert.AreEqual(expected, actual);
        }
    }
}
