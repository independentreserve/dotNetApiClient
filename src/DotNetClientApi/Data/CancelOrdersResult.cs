using System;
using System.Collections.Generic;

namespace IndependentReserve.DotNetClientApi.Data
{

    /// <summary>
    /// The result of canceling a set of orders
    /// </summary>
    public class CancelOrdersResult : Dictionary<Guid, CancelOrderResult>
    {
    }

    /// <summary>
    /// Order cancellation result
    /// </summary>
    public class CancelOrderResult
    {

        /// <summary>
        /// True - if the order was successfully canceled, False otherwise
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Message in case of error when canceling an order
        /// </summary>
        public string Message { get; set; }
    }

}