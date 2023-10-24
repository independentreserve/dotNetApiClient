namespace IndependentReserve.DotNetClientApi.Data
{
    public class CurrencyDecimalPlaces
    {
        /// <summary>
        /// Number of decimal places accepted for order volume when denominated in primary currency
        /// </summary>
        public int OrderPrimaryCurrency { get; set; }

        /// <summary>
        /// Number of decimal places accepted for price denominated in secondary currency
        /// </summary>
        public int OrderSecondaryCurrency { get; set; }
    }
}