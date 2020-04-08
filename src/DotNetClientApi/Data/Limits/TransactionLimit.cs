namespace IndependentReserve.DotNetClientApi.Data.Limits
{
    public abstract class TransactionLimit
    {
        public string Period { get; set; }

        public decimal AutomaticApprovalLimit { get; set; }
    }
}
