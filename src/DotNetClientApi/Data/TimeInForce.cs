using System.ComponentModel;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Defines limit order behavior in exchange.
    /// </summary>
    public enum TimeInForce
    {
        None = 0,

        /// <summary>
        /// Good till cancelled.
        /// The order is placed on the book until it is canceled.
        /// This is the default and is currently how our limit orders behave.
        /// (This option is used for backwards compatibility when no timeInForce is specified in the place order request). 
        /// </summary>
        [Description("Good till cancelled")] 
        Gtc = 1,

        /// <summary>
        /// Immediate or cancel.
        /// If any volume from the order can be executed immediately it will be executed.
        /// If there is any remaining unfilled volume in the order, the order will be cancelled
        /// instead of being placed on the orderbook.
        /// </summary>
        [Description("Immediate or cancel")] 
        Ioc = 2,

        /// <summary>
        /// Fill or kill.
        /// The order is canceled if it's not executed fully immediately at the time of placement. 
        /// </summary>
        [Description("Fill or kill")] 
        Fok = 3,

        /// <summary>
        /// Maker or cancel.
        /// The order will be placed on the orderbook fully or cancelled, the order will not execute immediately.
        /// </summary>
        [Description("Maker or cancel")]
        Moc = 4
    }
}