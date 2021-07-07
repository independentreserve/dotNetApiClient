namespace IndependentReserve.DotNetClientApi.Data
{
    public class WithdrawalFee
    {
        /// <summary>
        /// The withdrawal method. (Wire Transfer, Osko, etc.)
        /// </summary>
        public string WithdrawalType { get; set; }

        /// <summary>
        /// Minimum withdrawal amount
        /// </summary>
        public decimal MinimumAmount { get; set; }

        /// <summary>
        /// The secondary currency
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// The withdrawal fee amount
        /// </summary>
        public FeeModel Fee { get; set; }
    }
}
