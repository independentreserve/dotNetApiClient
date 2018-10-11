namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Represents an order on the OrderBook
    /// </summary>
    public class OrderBookItem : OrderBookItemBase
    {
        /// <summary>
        /// Buy or Sell order
        /// </summary>
        public OrderType OrderType { get; set; }
    }
}
