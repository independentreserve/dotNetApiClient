namespace IndependentReserve.DotNetClientApi.Data
{
    public enum CurrencyType
    {
        Unspecified = 0,

        /// <summary>
        /// Crypto currencies - eg: Xbt
        /// </summary>
        Primary,

        /// <summary>
        /// Fiat currencies - eg: Aud, Usd
        /// </summary>
        Secondary
    }
}
