using System;
using System.Runtime.Serialization;

namespace IndependentReserve.DotNetClientApi.Data
{
    public class CryptoWithdrawal
    {
        /// <summary>
        /// The transaction guid for withdrawing.
        /// </summary>
        [DataMember]
        public Guid BankTransactionGuid { get; set; }

        /// <summary>
        /// Crypto currency where withdrawal is made.
        /// </summary>
        [DataMember]
        public string PrimaryCurrencyCode { get; set; }

        /// <summary>
        /// When the withdrawal was created.
        /// </summary>
        [DataMember]
        public DateTime CreatedTimestampUtc { get; set; }

        /// <summary>
        /// The amount that user wishes to transfer and to receive to his wallet.
        /// It doesn't include the fee.
        /// </summary>
        [DataMember]
        public decimal TotalWithdrawalAmount { get; set; }

        /// <summary>
        /// This fee is taken from user.
        /// </summary>
        [DataMember]
        public decimal WithdrawalFeeAmount { get; set; }

        /// <summary>
        /// The destination address.
        /// </summary>
        [DataMember]
        public string DestinationAddress { get; set; }

        /// <summary>
        /// For XRP/EOS/XLM
        /// </summary>
        [DataMember]
        public string DestinationTag { get; set; }

        /// <summary>
        /// The withdrawal status.
        /// </summary>
        [DataMember]
        public string PendingWithdrawalStatus { get; set; }

        /// <summary>
        /// Transaction id.
        /// </summary>
        [DataMember]
        public string TransactionId { get; set; }

        /// <summary>
        /// Contains hash reference to withdrawal transaction itself.
        /// </summary>
        [DataMember]
        public string TransactionHash { get; set; }

        /// <summary>
        /// The index of the transaction output to the destination address.
        /// </summary>
        [DataMember]
        public int? TransactionOutputIndex { get; set; }

        /// <summary>
        /// User specified withdrawal id
        /// </summary>
        [DataMember]
        public string ClientWithdrawalId { get; set; }
    }
}
