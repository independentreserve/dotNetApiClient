using System;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Bank order (existing)
    /// </summary>
    public class BankHistoryOrder
    {
        /// <summary>
        /// Timestamp in utc when this order was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
        
        /// <summary>
        /// Order type
        /// </summary>
        public OrderType OrderType { get; set; }
        
        /// <summary>
        /// Volume of order
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// The order's volume outstanding.
        /// </summary>
        public decimal? Outstanding { get; set; }

        /// <summary>
        /// The limit order's ask/bid price; null for market orders
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// The order's overall price (weighted average).
        /// </summary>
        public decimal? AvgPrice { get; set; }

        /// <summary>
        /// The order's total value (sum of executed trades values).
        /// </summary>
        public decimal? Value { get; set; }
        
        /// <summary>
        /// The order's status
        /// </summary>
        public OrderStatus Status { get; set; }
        
        /// <summary>
        /// The order's guid
        /// </summary>
        public Guid OrderGuid { get; set; }
        
        /// <summary>
        /// Order's digital currency
        /// </summary>
        public CurrencyCode PrimaryCurrencyCode { get; set; }
        
        /// <summary>
        /// Order's fiat currency
        /// </summary>
        public CurrencyCode SecondaryCurrencyCode { get; set; }
    }
}
