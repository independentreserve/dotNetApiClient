namespace IndependentReserve.DotNetClientApi.Data
{
    public class DepositFee
    {        
        /// <summary>
        /// The deposit type. (Swift, Eft, Osko, etc.)
        /// </summary>
        public string DepositType { get; set; }
        /// <summary>
        /// The minimum deposit amount under which fee will be taken.
        /// This value is null if fee is always applied
        /// </summary>
        public decimal? FreeThreshold { get; set; }
        /// <summary>
        /// The deposit fee amount which will be substracted deposit amount.
        /// </summary>
        public decimal Amount { get; set; }
    }
}
