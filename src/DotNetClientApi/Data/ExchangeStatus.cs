using System.Collections.Generic;

namespace IndependentReserve.DotNetClientApi.Data
{

    public class ExchangeStatus
    {
        public bool IsOverallHealthy { get; set; }
        /// <summary>
        /// The collection contains entries indicating whether the trading is enabled for corresponding currency
        /// </summary>
        public Dictionary<CurrencyCode, bool> TradingEnabled { get; set; }
        /// <summary>
        /// The collection contains entries indicating whether withdrawals can be made in a corresponding currency
        /// </summary>
        public Dictionary<CurrencyCode, bool> WithdrawalsEnabled { get; set; }
        /// <summary>
        /// The collection contains entries indicating whether deposits can be made in a corresponding currency
        /// </summary>
        public Dictionary<CurrencyCode, bool> DepositsEnabled { get; set; }
    }
}
