namespace IndependentReserve.DotNetClientApi.Data
{
    public class BankOrderVolume
    {
        /// <summary>
        /// <see cref="VolumeCurrencyType"/> volume of order
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// The order's volume outstanding in <see cref="VolumeCurrencyType"/> currency
        /// </summary>
        public decimal? Outstanding { get; set; }

        /// <summary>
        /// Volume currency discriminator. 
        /// </summary>
        public CurrencyType VolumeCurrencyType { get; set; }
    }
}
