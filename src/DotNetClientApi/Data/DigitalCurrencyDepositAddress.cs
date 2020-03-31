using System;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Digital Currency address used for deposits into your XBT or ETH IR account
    /// </summary>
    public class DigitalCurrencyDepositAddress
    {
        /// <summary>
        /// Deposit address
        /// </summary>
        public string DepositAddress { get; set; }

        /// <summary>
        /// Destionation tag. 
        /// </summary>
        /// <remarks>Field is returned only for currecies that support destination tags (XRP, XLM, EOS, etc.)</remarks>
        public string Tag { get; set; }
        
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
