using System;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Represents an order on the OrderBook.
    /// Order unique identifier added.
    /// </summary>
    public class OrderBookItemDetailed : OrderBookItemBase
    {
        /// <summary>
        /// Order Unique Identifier
        /// </summary>
        public Guid Guid { get; set; }
    }
}
