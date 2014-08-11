namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Defines possible transaction statuses
    /// </summary>
    public enum TransactionStatus
    {
        Pending = 0,
        Unconfirmed = 1,
        Confirmed = 2,
        Disappeared = 3,
        Rejected = 4,
    }
}
