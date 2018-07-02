using System;
using System.Globalization;
using System.Threading;
using IndependentReserve.DotNetClientApi;
using IndependentReserve.DotNetClientApi.Extensions;
using NUnit.Framework;

namespace UnitTest
{
    /// <summary>
    ///     The test fixture base class.
    /// </summary>
    [TestFixture]
    public abstract class FixtureBase
    {
        /// <summary>
        /// The test fixture setup method.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            //  set culture to en-US
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }


        /// <summary>
        /// Requires environment variable:
        /// 
        /// [Environment]::SetEnvironmentVariable("IR_DOTNETCLIENTAPI_TEST_CONFIG", "url,key,secret", "User")
        /// 
        /// and then restart Visual Studio to pick up the change
        /// </summary>
        protected ApiConfig GetConfig()
        {
            var envVarKey = "IR_DOTNETCLIENTAPI_TEST_CONFIG";
            var envVar = Environment.GetEnvironmentVariable(envVarKey);
            if (string.IsNullOrEmpty(envVar))
            {
                throw new Exception($"Unit tests require environment variable {envVarKey}");
            }
            var config = ApiConfigExtensions.FromCsv(envVar);
            return config;
        }
    }
}
