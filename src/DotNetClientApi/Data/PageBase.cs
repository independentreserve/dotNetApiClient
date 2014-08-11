namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Base class for generic page class
    /// </summary>
    public abstract class PageBase
    {
        public long PageSize { get; set; }
        public long TotalItems { get; set; }
        public long TotalPages { get; set; }
    }
}