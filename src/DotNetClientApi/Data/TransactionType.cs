namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Defines possible transaction types
    /// </summary>
    public enum TransactionType
    {
        Deposit = 0,
        Withdrawal = 1,
        Trade = 2,
        Transfer = 3,
        Brokerage = 4,
        WithdrawalFee = 5,
        BitcoinNetworkFee = 6,
        Commission = 7,
        GST = 8,
        Unclaimed = 9,
        Error = 10,
        DepositFee = 11,
        ReferralCommission = 12,
        AccountFee = 13,
        StatementFee = 14,
        ReferralBonus = 15,
        Buy = 16,
        Sell = 17,
        PositionOpeningFee = 18,
        PositionClosingFee = 19,
        PositionLiquidationFee = 20,
        Interest = 21,
        Loan = 22,
    }
}
