namespace IndependentReserve.DotNetClientApi.Data.Common
{
    public class TotalFee
    {
        /// <summary>
        /// Total amount (inlcusive of any fees)
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Fee amount which will be taken out of the total amount
        /// </summary>
        public decimal Fee { get; set; }

    }
}
