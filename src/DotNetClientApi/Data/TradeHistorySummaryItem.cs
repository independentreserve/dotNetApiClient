using System;

namespace IndependentReserve.DotNetClientApi.Data
{
    public class TradeHistorySummaryItem
    {
        /// <summary>
        /// start time of time period this item represents
        /// </summary>
        public DateTime StartTimestampUtc { get; set; }

        /// <summary>
        /// end time of time period this item represents
        /// </summary>
        public DateTime EndTimestampUtc { get; set; }

        /// <summary>
        /// volume traded in primary currency during period
        /// </summary>
        public Decimal PrimaryCurrencyVolume { get; set; }

        /// <summary>
        /// volume traded in secondary currency during period
        /// </summary>
        public Decimal SecondaryCurrencyVolume { get; set; }

        /// <summary>
        /// number of trades executed during period
        /// </summary>
        public long NumberOfTrades { get; set; }

        /// <summary>
        /// highest secondary price reached  during period
        /// </summary>
        public Decimal HighestSecondaryCurrencyPrice { get; set; }

        /// <summary>
        /// Lowest secondary price reached during period
        /// </summary>
        public Decimal LowestSecondaryCurrencyPrice { get; set; }

        /// <summary>
        /// secondary currency price at opening of period
        /// </summary>
        public Decimal OpeningSecondaryCurrencyPrice { get; set; }

        /// <summary>
        /// secondary currency price at closing of period
        /// </summary>
        public Decimal ClosingSecondaryCurrencyPrice { get; set; }

        /// <summary>
        /// This is a weighted average, based on volume of trades and the price of each trade
        /// </summary>
        public Decimal AverageSecondaryCurrencyPrice { get; set; }
    }
}
