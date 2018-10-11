namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Represents an order on the OrderBook
    /// </summary>
    public class OrderBookItemBase
    {
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
