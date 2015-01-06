using System;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Contains trade details accompanied with order information.
    /// It represents only one side of a Trade made, in case of bid & ask belonging to the same user 
    /// - there should be two different objects of TradeDetails type - one for bid trade and another for ask trade.
    /// </summary>
    public class TradeDetails
    {
        /// <summary>
        /// The trade GUID.
        /// </summary>
        public Guid TradeGuid { get; set; }

        /// <summary>
        /// The date when order was filled (traded executed).
        /// </summary>
        public DateTime TradeTimestampUtc { get; set; }

        /// <summary>
        /// The order GUID.
        /// </summary>
        public Guid OrderGuid { get; set; }

        /// <summary>
        /// Order type.
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// The date when order was created.
        /// </summary>
        public DateTime OrderTimestampUtc { get; set; }

        /// <summary>
        /// Volume traded.
        /// </summary>
        public decimal VolumeTraded { get; set; }

        /// <summary>
        /// Price of the deal.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The primary currency code.
        /// </summary>
        public string PrimaryCurrencyCode { get; set; }

        /// <summary>
        /// The secondary currency code.
        /// </summary>
        public string SecondaryCurrencyCode { get; set; }
    }
}
