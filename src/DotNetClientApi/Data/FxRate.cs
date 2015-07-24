namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Represents exchange rate between two currencies.
    /// </summary>
    public class FxRate
    {
        /// <summary>
        /// Primary currency code.
        /// </summary>
        public string CurrencyCodeA { get; set; }

        /// <summary>
        /// Secondary currency code.
        /// </summary>
        public string CurrencyCodeB { get; set; }

        /// <summary>
        /// Exchange rate between two currencies.
        /// </summary>
        public decimal Rate { get; set; }
    }
}