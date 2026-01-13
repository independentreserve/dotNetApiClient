using System;

namespace IndependentReserve.DotNetClientApi.Data
{
    public class DealTransactions
    {
        /// <summary>
        /// The deal GUID
        /// </summary>
        public Guid DealGuid { get; set; }

        /// <summary>
        /// The deal transactions
        /// </summary>
        public Transaction[] Transactions { get; set; }
    }
}