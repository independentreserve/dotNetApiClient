using System;
using System.Configuration;
using IndependentReserve.DotNetClientApi;

namespace SampleApplication
{
    public class AppSettingsConfigProvider : IConfigProvider
    {
        public ApiConfig Get()
        {
            var creds = new ApiCredential
            {
                Key = ConfigurationManager.AppSettings["apiKey"]
                ,Secret = ConfigurationManager.AppSettings["apiSecret"]
            };

            var config = new ApiConfig
            {
                BaseUrl = ConfigurationManager.AppSettings["apiUrl"]
                ,Credential = creds
                ,NonceExpiryMode = (NonceExpiryMode) Enum.Parse(typeof(NonceExpiryMode), ConfigurationManager.AppSettings["nonceExpiryMode"])
            };

            return config;
        }
    }
}
