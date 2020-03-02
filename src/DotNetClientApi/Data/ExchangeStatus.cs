using System.Collections.Generic;

namespace IndependentReserve.DotNetClientApi.Data
{
    public class ExchangeStatus
    {
        public bool IsOverallHealthy { get; set; }

        public Dictionary<CurrencyCode, bool> TradingEnabled { get; set; }

        public Dictionary<CurrencyCode, bool> WithdrawalsEnabled { get; set; }

        public Dictionary<CurrencyCode, bool> DepositsEnabled { get; set; }
    }
}
