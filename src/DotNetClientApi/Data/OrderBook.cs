using System;
using System.Collections.Generic;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Represents the Order Book
    /// </summary>
    public class OrderBook
    {
        /// <summary>
        /// List of Buy Orders
        /// </summary>
        public List<OrderBookItem> BuyOrders { get; set; }

        /// <summary>
        /// List of Sell Orders
        /// </summary>
        public List<OrderBookItem> SellOrders { get; set; }

        /// <summary>
        /// Primary Currency this Orderbook is in
        /// </summary>
        public string PrimaryCurrencyCode { get; set; }

        /// <summary>
        /// Secondary Currency this Orderbook is in
        /// </summary>
        public string SecondaryCurrencyCode { get; set; }

        /// <summary>
        /// UTC Timestamp when this was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
    }
}
