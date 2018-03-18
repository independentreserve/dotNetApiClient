using IndependentReserve.DotNetClientApi;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestFixture]
    class HttpWorkerFixture
    {
        [Test]
        public void HmacSha256Hash()
        {
            var output = HttpWorker.GetHmacSha256Hash("message", "key");
            Assert.AreEqual("6E9EF29B75FFFC5B7ABAE527D58FDADB2FE42E7219011976917343065F58ED4A", output);
        }

    }
}
