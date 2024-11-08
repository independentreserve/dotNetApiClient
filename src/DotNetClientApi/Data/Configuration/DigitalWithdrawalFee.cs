using IndependentReserve.DotNetClientApi.Data.Common;

namespace IndependentReserve.DotNetClientApi.Data.Configuration
{
    public class DigitalWithdrawalFee : DigitalCurrency
    {
        public decimal Fee { get; set; }
    }
}
