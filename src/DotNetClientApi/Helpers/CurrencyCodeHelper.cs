using IndependentReserve.DotNetClientApi.Data;
using System;

namespace IndependentReserve.DotNetClientApi.Helpers
{
    internal static class CurrencyCodeHelper
    {
        public static CurrencyCode Parse(string ticker)
        {
            if (string.IsNullOrEmpty(ticker))
            {
                throw new ArgumentNullException(nameof(ticker));
            }

            if (Enum.TryParse(ticker, false, out CurrencyCode currencyCode))
            {
                return currencyCode;
            }

            //Transform the ticker to the integer using FNV-1a hash
            var hash = (int)Fnv1a.GetFnv1aHashCode(ticker);
            return (CurrencyCode)hash;
        }
    }
}
