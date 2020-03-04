using System.Collections.Generic;

namespace IndependentReserve.DotNetClientApi.Data
{

    public class ExchangeStatus
    {
        /// <summary>
        /// Overall health of the exchange
        /// </summary>
        public bool IsOverallHealthy { get; set; }

        /// <summary>
        /// Indicates whether trading is enabled for each currency
        /// </summary>
        public Dictionary<CurrencyCode, bool> TradingEnabled { get; set; }

        /// <summary>
        ///Indicates whether withdrawals are enabled for each currency
        /// </summary>
        public Dictionary<CurrencyCode, bool> WithdrawalsEnabled { get; set; }

        /// <summary>
        /// Indicates whether deposits are enabled for each currency
        /// </summary>
        public Dictionary<CurrencyCode, bool> DepositsEnabled { get; set; }
    }
}
