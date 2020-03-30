namespace IndependentReserve.DotNetClientApi.Data
{
    public class DigitalWithdrawalRequest
    {
        public CurrencyCode Currency { get; set; }

        public decimal Amount { get; set; }
        
        public string Address { get; set; }

        public string Comment { get; set; }

        /// <summary>
        /// Leave empty if currency does not support tags
        /// </summary>
        public string DestinationTag { get; set; }

        public string ClientWithdrowalId { get; set; }
    }
}
