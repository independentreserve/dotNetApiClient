using System.Collections.Generic;

namespace IndependentReserve.DotNetClientApi.Data.Limits
{
    public class DepositLimits
    {
        public List<FiatDepositLimit> Fiat { get; set; }
    }
}
