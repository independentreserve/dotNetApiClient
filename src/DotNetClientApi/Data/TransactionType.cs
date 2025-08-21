namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Defines the possible transaction types.
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// A deposit from an external source.
        /// </summary>
        Deposit = 0,

        /// <summary>
        /// A withdrawal to an external destination.
        /// </summary>
        Withdrawal = 1,

        /// <summary>
        /// A transaction that is the result of a trade on the exchange.
        /// A single trade is represented by two Trade transactions: one fiat and ony crypto.
        /// </summary>
        Trade = 2,

        /// <summary>
        /// Not used, or for system use only.
        /// </summary>
        Transfer = 3,

        /// <summary>
        /// Trading fee.
        /// </summary>
        Brokerage = 4,

        /// <summary>
        /// A withdrawal fee.
        /// </summary>
        WithdrawalFee = 5,

        /// <summary>
        /// Not used, or for system use only.
        /// </summary>
        BitcoinNetworkFee = 6,

        /// <summary>
        /// Not used, or for system use only.
        /// </summary>
        Commission = 7,

        /// <summary>
        /// Tax applied to brokerage and other fees.
        /// </summary>
        GST = 8,

        /// <summary>
        /// Not used, or for system use only.
        /// </summary>
        Unclaimed = 9,

        /// <summary>
        /// Not used, or for system use only.
        /// </summary>
        Error = 10,

        /// <summary>
        /// Deposit processing fee, usually applied to small fiat deposits.
        /// </summary>
        DepositFee = 11,

        /// <summary>
        /// Referral commission returned to the referral user.
        /// </summary>
        ReferralCommission = 12,

        /// <summary>
        /// Not used, or for system use only.
        /// </summary>
        AccountFee = 13,

        /// <summary>
        /// Annual tax report fee.
        /// </summary>
        StatementFee = 14,

        /// <summary>
        /// Referral bonus applied to a parent or child user.
        /// </summary>
        ReferralBonus = 15,

        /// <summary>
        /// Represents buying a crypto asset.
        /// Both debit and credit transactions will use the Buy transaction type.
        /// </summary>
        Buy = 16,

        /// <summary>
        /// Represents selling a crypto asset.
        /// Both debit and credit transactions will use the Sell transaction type.
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
        /// Leverage trading. Interest applied to the borrowed funds.
        /// </summary>
        Interest = 21,

        /// <summary>
        /// Leverage trading. Borrowed funds.
        /// </summary>
        Loan = 22,
    }
}
