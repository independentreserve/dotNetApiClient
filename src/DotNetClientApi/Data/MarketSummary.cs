using System;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Represents current market summary
    /// </summary>
    public class MarketSummary
    {
        /// <summary>
        /// The 24 hours highest bitcoin price.
        /// </summary>
        public decimal? DayHighestPrice { get; set; }

        /// <summary>
        /// The 24 hours lowest bitcoin price.
        /// </summary>
        public decimal? DayLowestPrice { get; set; }

        /// <summary>
        /// The 24 hours average bitcoin price.
        /// </summary>
        public decimal? DayAvgPrice { get; set; }

        /// <summary>
        /// The 24 hours trade volume.
        /// </summary>
        public decimal? DayVolumeXbt { get; set; }

        /// <summary>
        /// The lowest traded offer price. Used as restriction to prevent cross-market.
        /// </summary>
        public decimal? CurrentLowestOfferPrice { get; set; }

        /// <summary>
        /// The highest traded bid price. Used as restriction to prevent cross-market.
        /// </summary>
        public decimal? CurrentHighestBidPrice { get; set; }

        /// <summary>
        /// The last traded bitcoin price.
        /// </summary>
        public decimal? LastPrice { get; set; }

        /// <summary>
        /// Primary Currency this MarketSummary is in
        /// </summary>
        public CurrencyCode PrimaryCurrencyCode { get; set; }

        /// <summary>
        /// Secondary Currency this MarketSummary is in
        /// </summary>
        public CurrencyCode SecondaryCurrencyCode { get; set; }

        /// <summary>
        /// UTC Timestamp when this was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
    }
}
