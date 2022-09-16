using System;

namespace IndependentReserve.DotNetClientApi.Data
{
    public class TransactionBase
    {
        /// <summary>
        /// Gets utc timestamp of when transaction was settled; after SettlementDate transaction is immutable
        /// </summary>
        public DateTime? SettleTimestampUtc { get; set; }

        /// <summary>
        /// Gets utc timestamp of when transaction was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }

        /// <summary>
        /// Gets type of transaction
        /// </summary>
        public TransactionType Type { get; set; }

        /// <summary>
        /// Gets status of transaction
        /// </summary>
        public TransactionStatus Status { get; set; }

        /// <summary>
        /// Gets currency of transaction
        /// </summary>
        public CurrencyCode CurrencyCode { get; set; }

        /// <summary>
        /// Gets credit amount of transaction
        /// </summary>
        public decimal? Credit { get; set; }

        /// <summary>
        /// Gets debit amount of transaction
        /// </summary>
        public decimal? Debit { get; set; }

        /// <summary>
        /// Gets balance of account as it was after this transaction
        /// </summary>
        public decimal? Balance { get; set; }

        /// <summary>
        /// Gets comment of this transaction
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// The transactions's guid
        /// </summary>
        public Guid TransactionGuid { get; set; }
    }
}
