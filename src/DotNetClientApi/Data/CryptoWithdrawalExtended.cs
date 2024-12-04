using System.Runtime.Serialization;
using IndependentReserve.DotNetClientApi.Data.Common;

namespace IndependentReserve.DotNetClientApi.Data
{
    public class CryptoWithdrawalExtended : CryptoWithdrawal
    {
        /// <summary>
        /// Contains hash reference to withdrawal transaction itself.
        /// </summary>
        [DataMember]
        public BlockchainTransaction Transaction { get; set; }
    }
}
