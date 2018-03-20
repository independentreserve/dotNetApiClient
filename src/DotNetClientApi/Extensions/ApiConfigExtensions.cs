using System;
using System.Collections.Generic;
using System.Text;

namespace IndependentReserve.DotNetClientApi.Extensions
{
    public static class ApiConfigExtensions
    {
        public static ApiConfig FromCsv(string csv)
        {
            var components = csv.Split(new char[] { ',', ';' });
            ApiConfig config;
            switch (components.Length)
            {
                case 1:
                    config = new ApiConfig(components[0]);
                    break;
                case 3:
                    config = new ApiConfig(components[0], components[1], components[2]);
                    break;
                default:
                    throw new NotSupportedException($"CSV split into length {components.Length} which was unexpected");
            }
            return config;
        }
    }
}
