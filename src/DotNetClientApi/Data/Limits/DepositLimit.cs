namespace IndependentReserve.DotNetClientApi.Data.Limits
{
    public class DepositLimit : TransactionLimit
    {
        public decimal Deposited { get; set; }
    }
}
