using System;
using System.Collections.Generic;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// A historical summary of trading data
    /// </summary>
    public class TradeHistorySummary
    {
        /// <summary>
        /// The items in the trade history
        /// </summary>
        public List<TradeHistorySummaryItem> HistorySummaryItems { get; set; }

        /// <summary>
        /// Number of hours in the past for whcih data retrieved
        /// </summary>
        public int NumberOfHoursInThePastToRetrieve { get; set; }

        /// <summary>
        /// Primary Currency this TradeHistorySummary is in
        /// </summary>
        public CurrencyCode PrimaryCurrencyCode { get; set; }

        /// <summary>
        /// Secondary Currency this TradeHistorySummary is in
        /// </summary>
        public CurrencyCode SecondaryCurrencyCode { get; set; }

        /// <summary>
        /// UTC Timestamp when this was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
    }
}
