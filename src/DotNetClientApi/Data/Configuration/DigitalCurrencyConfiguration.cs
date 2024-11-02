namespace IndependentReserve.DotNetClientApi.Data.Configuration
{
    public class DigitalCurrencyConfiguration
    {
        public string Currency { get; set; }

        public string Name { get; set; }

        public bool IsTradeEnabled { get; set; }

        public CurrencyDecimalPlaces DecimalPlaces { get; set; }

        public NetworkConfiguration[] Networks { get; set; }
    }
}
