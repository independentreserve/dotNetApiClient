namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Defines possible bank order statuses
    /// </summary>
    public enum OrderStatus
    {
        Open = 0,
        Filled = 1,
        PartiallyFilled = 2,
        PartiallyFilledAndCancelled = 3,
        Cancelled = 4,
        Expired = 5,
        PartiallyFilledAndExpired = 6,
        Failed = 7,
        PartiallyFilledAndFailed = 8,
    }
}
