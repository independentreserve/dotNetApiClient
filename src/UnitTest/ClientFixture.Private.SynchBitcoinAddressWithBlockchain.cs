using NUnit.Framework;

namespace UnitTest
{
    partial class ClientFixture
    {
        [Test]
        public void SynchBitcoinAddressWithBlockchain()
        {
            using (var client = CreatePrivateClient())
            {
                var bitcoinDepositAddress = client.GetBitcoinDepositAddress();

                Assert.IsNotNull(bitcoinDepositAddress);

                var synced = client.SynchBitcoinAddressWithBlockchain(bitcoinDepositAddress.BitcoinAddress);

                Assert.IsNotNull(synced);
                Assert.AreEqual(synced.BitcoinAddress, bitcoinDepositAddress.BitcoinAddress);
            }
        }
    }
}