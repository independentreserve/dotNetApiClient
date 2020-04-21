namespace IndependentReserve.DotNetClientApi.Data
{
    public class BankOrderVolume
    {
        /// <summary>
        /// <see cref="VolumeCurrencyType"/> volume of order
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// The order's <see cref="OriginalVolumeCurrencyType"/> volume outstanding in 
        /// </summary>
        public decimal? Outstanding { get; set; }

        /// <summary>
        /// Volume currency discriminator. 
        /// </summary>
        public CurrencyType VolumeCurrencyType { get; set; }
    }
}
