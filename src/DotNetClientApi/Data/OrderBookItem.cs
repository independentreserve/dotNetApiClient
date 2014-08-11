namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Represents an order on the OrderBook
    /// </summary>
    public class OrderBookItem
    {
        /// <summary>
        /// Buy or Sell order
        /// </summary>
        public OrderType OrderType { get; set; }

        /// <summary>
        /// Price in Secondary Currency
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Volume in Primary Currency
        /// </summary>
        public decimal Volume { get; set; }
    }
}
