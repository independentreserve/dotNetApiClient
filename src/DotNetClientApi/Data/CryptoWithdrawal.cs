using IndependentReserve.DotNetClientApi.Data.Common;
using System;
using System.Runtime.Serialization;

namespace IndependentReserve.DotNetClientApi.Data
{
    public class CryptoWithdrawal
    {
        /// <summary>
        /// The bank transaction guid for withdrawing.
        /// </summary>
        [DataMember]
        public Guid TransactionGuid { get; set; }

        /// <summary>
        /// Crypto currency where withdrawal is made.
        /// </summary>
        [DataMember]
        public string PrimaryCurrencyCode { get; set; }

        /// <summary>
        /// Blockchain network where withdrawal is made.
        /// </summary>
        [DataMember]
        public string Network { get; set; }

        /// <summary>
        /// When the pending withdrawal was created.
        /// </summary>
        [DataMember]
        public DateTime CreatedTimestampUtc { get; set; }

        /// <summary>
        /// The amount that user wishes to transfer and to receive to his wallet.
        /// </summary>
        [DataMember]
        public TotalFee Amount { get; set; }

        /// <summary>
        /// The destination address.
        /// </summary>
        [DataMember]
        public DigitalAddress Destination { get; set; }

        /// <summary>
        /// The pending withdrawal status.
        /// </summary>
        [DataMember]
        public string Status { get; set; }
    }
}
