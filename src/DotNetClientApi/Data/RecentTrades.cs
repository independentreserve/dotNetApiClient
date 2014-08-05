using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndependentReserve.DotNetClientApi.Data
{
    public class RecentTrades
    {
        /// <summary>
        /// List of Recent trades
        /// </summary>
        public List<Trade> Trades { get; set; }

        /// <summary>
        /// Primary Currency the trades are in
        /// </summary>
        public string PrimaryCurrencyCode { get; set; }

        /// <summary>
        /// Secondary Currency the trades are in
        /// </summary>
        public string SecondaryCurrencyCode { get; set; }

        /// <summary>
        /// UTC Timestamp when this was created
        /// </summary>
        public DateTime? CreatedTimestampUtc { get; set; }
    }
}
