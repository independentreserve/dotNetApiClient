using NUnit.Framework;

namespace UnitTest
{
    partial class ClientFixture
    {
        [Test]
        public void GetBitcoinDepositAddress()
        {
            using (var client = CreatePrivateClient())
            {
                var bitcoinDepositAddress = client.GetBitcoinDepositAddress();

                Assert.IsNotNull(bitcoinDepositAddress);
            }
        }
    }
}
