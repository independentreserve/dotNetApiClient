using System;
using System.Collections.Generic;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Represents the Order Book
    /// </summary>
    public class OrderBookBase<T>
    {
        /// <summary>
        /// List of Buy Orders
        /// </summary>
        public List<T> BuyOrders { get; set; }

        /// <summary>
        /// List of Sell Orders
        /// </summary>
        public List<T> SellOrders { get; set; }

        /// <summary>
        /// Primary Currency this Orderbook is in
        /// </summary>
        public CurrencyCode PrimaryCurrencyCode { get; set; }

        /// <summary>
        /// Secondary Currency this Orderbook is in
        /// </summary>
        public CurrencyCode SecondaryCurrencyCode { get; set; }

        /// <summary>
        /// UTC Timestamp when this was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
    }
}
