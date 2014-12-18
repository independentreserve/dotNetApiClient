using System;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Represents a request by a user to withdraw fiat currency from IR to their own bank account
    ///  </summary>
    public class FiatWithdrawalRequest
    {
        /// <summary>
        /// Unique ID of this request 
        /// </summary>
        public Guid FiatWithdrawalRequestGuid { get; set; }

        /// <summary>
        /// The IR account to withdraw from
        /// </summary>
        public Guid AccountGuid { get; set; }

        /// <summary>
        /// The request status in the workflow
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Timestamp in UTC when fiat withdrawal request was created
        /// </summary>
        public DateTimeOffset CreatedTimestampUtc { get; set; }

        /// <summary>
        /// Total amount being withdrawn by the user (inlcusive of any fees)
        /// </summary>
        public Decimal TotalWithdrawalAmount { get; set; }

        /// <summary>
        /// Fee amount which will be taken out of the withdrawal amount
        /// </summary>
        public Decimal FeeAmount { get; set; }

        /// <summary>
        /// Currency being withdrawn
        /// </summary>
        public string Currency { get; set; }
    }
}