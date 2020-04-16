using System;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Bank order (newly created)
    /// </summary>
    public class BankOrder
    {
        /// <summary>
        /// Gets order guid
        /// </summary>
        public Guid OrderGuid { get; set; }

        /// <summary>
        /// Timestamp in UTC when order was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }

        /// <summary>
        /// Order's type
        /// </summary>
        public OrderType Type { get; set; }

        /// <summary>
        /// Volume ordered
        /// </summary>
        public decimal VolumeOrdered { get; set; }

        /// <summary>
        /// Volume filled
        /// </summary>
        public decimal VolumeFilled { get; set; }

        /// <summary>
        /// Order's price (for limit orders)
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// The order's overall price (weighted average).
        /// </summary>
        public decimal? AvgPrice { get; set; }

        /// <summary>
        /// Order's reserved amount (for limit orders)
        /// </summary>
        public decimal ReservedAmount { get; set; }

        /// <summary>
        /// Order's status
        /// </summary>
        public OrderStatus Status { get; set; }
     
        /// <summary>
        /// Order's digital currency
        /// </summary>
        public CurrencyCode PrimaryCurrencyCode { get; set; }
        
        /// <summary>
        /// Order's fiat currency
        /// </summary>
        public CurrencyCode SecondaryCurrencyCode { get; set; }

        /// <summary>
        /// Order fee percent
        /// </summary>
        public decimal FeePercent { get; set; }

        /// <summary>
        /// Volume currency discriminator. Possible values 'Primary', 'Secondary' 
        /// </summary>
        public string VolumeCurrencyType { get; set; }
    }
}
