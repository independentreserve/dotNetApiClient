namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// The possible states an order can take
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Order that has not yet executed, the initial state of an order
        /// </summary>
        Open = 0,

        /// <summary>
        /// An executed order, the full Volume on offer or bid upon has been met. Once in this state, the order is immutable.
        /// </summary>
        Filled = 1,

        /// <summary>
        /// The order executed but so far only part of the Volume on offer or bid has been met. 
        /// An order in this state may be executed against multiple times until it will become Filled or PartiallyFilledAndFailed.
        /// </summary>
        PartiallyFilled = 2,

        /// <summary>
        /// The order executed executed, and part of the Volume on offer or bid has been met.
        /// The user cancelled the order after partial execution. An order in this state will be not executed anymore.
        /// </summary>
        PartiallyFilledAndCancelled = 3,

        /// <summary>
        /// Cancelled by user before any execution
        /// </summary>
        Cancelled = 4,

        /// <summary>
        /// Order has expired and will not execute
        /// </summary>
        Expired = 5,

        /// <summary>
        /// An order in this state will not execute further due to expiry
        /// </summary>
        PartiallyFilledAndExpired = 6,

        /// <summary>
        /// Order failed to execute 
        /// </summary>
        Failed = 7,

        /// <summary>
        /// Order was partially executed but later failed and will not execute further
        /// </summary>
        /// <remarks>
        /// Possible reasons for this state
        /// 
        /// * Market order hit slippage failsafe limit
        /// * Market order left consumed all remaining limit orders on other side of the book
        /// * Market order exceeded available balance on account
        /// * Unspecified exception within the exchange system
        /// </remarks>
        PartiallyFilledAndFailed = 8,
    }
}
