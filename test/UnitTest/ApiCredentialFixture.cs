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
    public class ApiCredentialFixture
    {

        [TestCase("1234567890", "1234")]
        [TestCase("1234", "1234")]
        [TestCase("123", "123")]
        [TestCase(null,"<nil>")]
        [TestCase(" ", "<nil>")]
        [TestCase("", "<nil>")]
        public void ToStringTest(string key, string expected)
        {
            var sut = new ApiCredential { Key = key };
            Assert.AreEqual(expected, sut.ToString());
        }
    }
}
