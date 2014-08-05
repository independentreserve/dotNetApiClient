using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace UnitTest
{
    /// <summary>
    ///     The test fixture base class.
    /// </summary>
    [TestFixture]
    public abstract class FixtureBase
    {
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
}
