namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// User's brokerage fee for a currency
    /// </summary>
    public class BrokerageFee
    {
        public CurrencyCode CurrencyCode { get; set; }

        public decimal Fee { get; set; }
    }
}
