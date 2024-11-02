namespace IndependentReserve.DotNetClientApi.Data.Limits
{
    public class DigitalCurrencyWithdrawalLimit : WithdrawalLimit
    {
        public string Network { get; set; }

        public string Currency { get; set; }
    }
}
