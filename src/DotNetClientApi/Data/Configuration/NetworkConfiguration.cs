namespace IndependentReserve.DotNetClientApi.Data.Configuration
{
    public class NetworkConfiguration
    {
        public string Network { get; set; }

        public bool IsDelisted { get; set; }

        public bool IsDepositEnabled { get; set; }

        public bool IsWithdrawalEnabled { get; set; }
    }
}
