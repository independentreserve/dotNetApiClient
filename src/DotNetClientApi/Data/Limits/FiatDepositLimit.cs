using System.Collections.Generic;

namespace IndependentReserve.DotNetClientApi.Data.Limits
{
    public class FiatDepositLimit
    {
        /// <summary>
        /// The deposit type. (Swift, Eft, Osko, etc.)
        /// </summary>
        public string DepositType { get; set; }

        /// <summary>
        /// Maximum possible deposit amount.
        /// </summary>
        public decimal MaxTransactionAmount { get; set; }
        
        /// <summary>
        /// Automatic approval limits
        /// </summary>
        public List<DepositLimit> Limits { get; set; }
    }
}
