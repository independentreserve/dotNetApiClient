using System;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Represent one trade action
    /// </summary>
    public class Trade
    {
        /// <summary>
        /// The date of the trade.
        /// </summary>
        public DateTime? TradeTimestampUtc { get; set; }

        /// <summary>
        /// The amount of primary currency that was traded
        /// </summary>
        public decimal PrimaryCurrencyAmount { get; set; }

        /// <summary>
        /// The price in secondary currency of the trade
        /// </summary>
        public decimal SecondaryCurrencyTradePrice { get; set; }
    }
}
