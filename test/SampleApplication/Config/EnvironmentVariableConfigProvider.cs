using System;
using IndependentReserve.DotNetClientApi;
using IndependentReserve.DotNetClientApi.Extensions;

namespace SampleApplication
{
    public class EnvironmentVariableConfigProvider : IConfigProvider
    {
        public ApiConfig Get()
        {
            var envVarKey = "IR_DOTNETCLIENTAPI_TEST_CONFIG";
            var envVar = Environment.GetEnvironmentVariable(envVarKey);
            var config = new ApiConfig();
            if (!string.IsNullOrEmpty(envVar))
            {
                config = ApiConfigExtensions.FromCsv(envVar);
                return config;
            }
            return config;
        }
    }
}
