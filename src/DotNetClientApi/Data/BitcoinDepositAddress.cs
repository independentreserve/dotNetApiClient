using System;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Bitcoin address used for deposits into your XBT IR account
    /// </summary>
    public class BitcoinDepositAddress
    {
        /// <summary>
        /// Bitcoin address
        /// </summary>
        public string BitcoinAddress { get; set; }
        
        /// <summary>
        /// Timestamp in UTC of when IR last checked this address for new deposits
        /// </summary>
        public DateTime? LastCheckedTimestampUtc { get; set; }
        
        /// <summary>
        /// Timestamp in UTC when IR will check this address for new deposits again
        /// </summary>
        public DateTime? NextUpdateTimestampUtc { get; set; }
    }
}
