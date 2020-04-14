namespace IndependentReserve.DotNetClientApi.Data.Limits
{
    public class DepositLimit : TransactionLimit
    {
        /// <summary>
        /// Deposits up to this amount made during a "Period" will be processed and credited to your account instantly
        /// </summary>
        public decimal Deposited { get; set; }
    }
}
