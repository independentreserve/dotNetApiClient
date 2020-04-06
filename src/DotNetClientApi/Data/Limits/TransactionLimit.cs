namespace IndependentReserve.DotNetClientApi.Data.Limits
{
    public class TransactionLimit
    {
        public string Period { get; set; }

        public decimal AutomaticApprovalLimit { get; set; }

        public decimal UsedLimit { get; set; }
    }
}
