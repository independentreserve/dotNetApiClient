namespace IndependentReserve.DotNetClientApi.Data.Limits
{
    public class WithdrawalLimit : TransactionLimit
    {
        public decimal Withdrawn { get; set; }
    }
}
