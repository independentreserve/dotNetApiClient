/*
  {
    "QuoteGuid": "23d7e735-b135-4408-9dbe-c0be28d930a6",
    "CreatedTimestampUtc": "2025-07-24T10:54:25.1393374+00:00",
    "MaxAgeMs": 60000,
    "PrimaryCurrencyCode": "Xbt",
    "SecondaryCurrencyCode": "Aud",
    "Side": "Buy",
    "Value": 544.3281,
    "Volume": 0.01
  }
*/

using System;
using IndependentReserve.DotNetClientApi.Data.Shop;

namespace IndependentReserve.DotNetClientApi.Data
{

    /// <summary>
    /// Represents a quote for trading
    /// </summary>
    public class Quote
    {
        /// <summary>
        /// The quote GUID
        /// </summary>
        public Guid QuoteGuid { get; set; }

        /// <summary>
        /// When the quote was created
        /// </summary>
        public DateTimeOffset CreatedTimestamp { get; set; }

        /// <summary>
        /// Maximum age of the quote in milliseconds
        /// </summary>
        public int MaxAgeMs { get; set; }

        /// <summary>
        /// The primary currency code
        /// </summary>
        public CurrencyCode PrimaryCurrencyCode { get; set; }

        /// <summary>
        /// The secondary currency code
        /// </summary>
        public CurrencyCode SecondaryCurrencyCode { get; set; }

        /// <summary>
        /// The side of the order (Buy/Sell)
        /// </summary>
        public TradeAction Side { get; set; }

        /// <summary>
        /// The total value of the quote
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// The volume to trade
        /// </summary>
        public decimal Volume { get; set; }
    }
} 
