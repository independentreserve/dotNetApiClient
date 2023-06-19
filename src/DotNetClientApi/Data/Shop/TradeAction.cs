namespace IndependentReserve.DotNetClientApi.Data.Shop
{
    public enum TradeAction
    {
        Unknown = 0,

        /// <summary>
        /// When a user is buying crypto.
        /// Corresponds to bid order types: LimitBid & MarketBid.
        /// Maps to OrderSide.Bid
        /// </summary>
        Buy = 1,

        /// <summary>
        /// When a user is selling crypto.
        /// Corresponds to offer order types: LimitOffer & MarketOffer.
        /// Maps to OrderSide.Offer
        /// </summary>
        Sell = 2,
    }
}
