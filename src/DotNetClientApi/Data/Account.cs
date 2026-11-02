using System;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Your Independent Reserve bank account in one currency; an account is created the first time you use its currency, so you may not have one in every currency
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Account guid
        /// </summary>
        public Guid AccountGuid { get; set; }

        /// <summary>
        /// Gets account status
        /// </summary>
        public AccountStatus AccountStatus { get; set; }

        /// <summary>
        /// Gets current available balance of this bank account, thats it total balance minus all funds reserved by account orders
        /// </summary>
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// Gets account's currency
        /// </summary>
        public CurrencyCode CurrencyCode { get; set; }

        /// <summary>
        /// Gets total current balance of this bank account
        /// </summary>
        public decimal TotalBalance { get; set; }

        public override string ToString() => $"{CurrencyCode}, Available={AvailableBalance}, Total={TotalBalance}";
    }
}
