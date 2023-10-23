namespace IndependentReserve.DotNetClientApi.Data
{
    public class CurrencyConfiguration
    {
        public string Code { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// When set to true, a permanent delisting has occurred. Only withdrawals are allowed, no further deposits or trading may occur
        /// </summary>
        public bool IsDelisted { get; set; }

        /// <summary>
        /// Indicates whether deposits are temporarily enabled/disabled
        /// </summary>
        public bool IsDepositEnabled { get; set; }

        /// <summary>
        /// Indicates whether withdrawals are temporarily enabled/disabled
        /// </summary>
        public bool IsWithdrawalEnabled { get; set; }

        /// <summary>
        /// Indicates whether trading is temporarily enabled/disabled
        /// </summary>
        public bool IsTradeEnabled { get; set; }

        public CurrencyDecimalPlaces DecimalPlaces { get; set; }
    }
}
