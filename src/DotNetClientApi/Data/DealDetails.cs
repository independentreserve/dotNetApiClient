/*
  {
    "DealGuid": "b5b0c52c-3100-4c13-abdd-959e99f97075",
    "CreatedTimestamp": "2025-07-24T10:48:07.1368695+00:00",
    "PrimaryCurrencyCode": "Xbt",
    "SecondaryCurrencyCode": "Aud",
    "Side": "Buy",
    "Value": 544.3281,
    "Volume": 0.01,
    "Status": "Completed"
  }
*/

using System;
using IndependentReserve.DotNetClientApi.Data.Shop;

namespace IndependentReserve.DotNetClientApi.Data
{

    /// <summary>
    /// Represents detailed deal information including execution details
    /// </summary>
    public class DealDetails
    {
        /// <summary>
        /// The deal GUID
        /// </summary>
        public Guid DealGuid { get; set; }

        /// <summary>
        /// When the quote was created
        /// </summary>
        public DateTimeOffset CreatedTimestamp { get; set; }

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
        /// The total value of the deal
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// The volume traded
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// The status of the deal
        /// </summary>
        public string Status { get; set; }
    }
} 
