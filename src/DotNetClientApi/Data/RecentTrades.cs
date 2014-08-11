using System;
using System.Collections.Generic;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Represents collection of recent trades
    /// </summary>
    public class RecentTrades
    {
        /// <summary>
        /// List of Recent trades
        /// </summary>
        public List<Trade> Trades { get; set; }

        /// <summary>
        /// Primary Currency the trades are in
        /// </summary>
        public CurrencyCode PrimaryCurrencyCode { get; set; }

        /// <summary>
        /// Secondary Currency the trades are in
        /// </summary>
        public CurrencyCode SecondaryCurrencyCode { get; set; }

        /// <summary>
        /// UTC Timestamp when this was created
        /// </summary>
        public DateTime? CreatedTimestampUtc { get; set; }
    }
}
