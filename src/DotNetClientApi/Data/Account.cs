using System;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Your independent reserve bank account; when you confirm your user account we create accounts for you in digital and fiat currencies
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets account guid
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
    }
}
