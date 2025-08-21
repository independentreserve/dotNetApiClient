namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// The set of possible transaction types. Used in the GetTransactions 'Type' response.
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// A deposit of either fiat or crypto.
        /// </summary>
        Deposit = 0,

        /// <summary>
        /// A withdrawal of either fiat or crypto.
        /// </summary>
        Withdrawal = 1,

        /// <summary>
        /// A transaction that is the result of an order being filled (fully or partially) on the exchange.
        /// A single trade on the exchange will result in two Trade transactions: one fiat and one crypto.
        /// </summary>
        Trade = 2,

        /// <summary>
        /// For internal use only; will not be seen by clients.
        /// </summary>
        Transfer = 3,

        /// <summary>
        /// The fee charged for trading.
        /// </summary>
        Brokerage = 4,

        /// <summary>
        /// The fee charged for a fiat or crypto withdrawal.
        /// </summary>
        WithdrawalFee = 5,

        /// <summary>
        /// For internal use only; will not be seen by clients.
        /// </summary>
        BitcoinNetworkFee = 6,

        /// <summary>
        /// Deprecated; will not be seen by clients.
        /// </summary>
        Commission = 7,

        /// <summary>
        /// Goods and Services Tax applied to brokerage and other services.
        /// </summary>
        GST = 8,

        /// <summary>
        /// For internal use only; will not be seen by clients.
        /// </summary>
        Unclaimed = 9,

        /// <summary>
        /// Deprecated; will not be seen by clients.
        /// </summary>
        Error = 10,

        /// <summary>
        /// Deposit processing fee, usually applied to small fiat deposits.
        /// </summary>
        DepositFee = 11,

        /// <summary>
        /// Referral commission paid to the user who referred a client.
        /// </summary>
        ReferralCommission = 12,

        /// <summary>
        /// Deprecated; will not be seen by clients.
        /// </summary>
        AccountFee = 13,

        /// <summary>
        /// Fee charged for generating certain statements, for example, the annual tax report.
        /// </summary>
        StatementFee = 14,

        /// <summary>
        /// Referral bonus applied to either a referring client or the referred client.
        /// </summary>
        ReferralBonus = 15,

        /// <summary>
        /// Represents the purchase of a crypto asset.
        /// Both debit and credit transactions use the Buy transaction type.
        /// </summary>
        Buy = 16,

        /// <summary>
        /// Represents the sale of a crypto asset.
        /// Both debit and credit transactions use the Sell transaction type.
        /// </summary>
        Sell = 17,

        /// <summary>
        /// Leverage trading. The fee may apply when opening a position.
        /// </summary>
        PositionOpeningFee = 18,

        /// <summary>
        /// Leverage trading. The fee may apply when closing a position manually.
        /// </summary>
        PositionClosingFee = 19,

        /// <summary>
        /// Leverage trading. The fee may apply when a position is forcibly closed due to a low margin level.
        /// </summary>
        PositionLiquidationFee = 20,

        /// <summary>
        /// Leverage trading. Interest applied to borrowed funds.
        /// </summary>
        Interest = 21,

        /// <summary>
        /// Leverage trading. Borrowed funds.
        /// </summary>
        Loan = 22,
    }
}
