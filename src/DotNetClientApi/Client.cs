using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IndependentReserve.DotNetClientApi.Data;
using Newtonsoft.Json;

namespace IndependentReserve.DotNetClientApi
{
    /// <summary>
    /// IndependentReserver API client, implements IDisposable
    /// </summary>
    public class Client:IDisposable
    {
        private ulong _nonce;
        private string _apiKey;
        private string _apiSecret;

        private HttpClient _client;

        #region private constructors
        /// <summary>
        /// Creates instance of Client class, which can be used then to call public ONLY api methdos
        /// </summary>
        /// <param name="baseUri"></param>
        private Client(Uri baseUri)
        {
            _nonce = (ulong)DateTime.UtcNow.Ticks;

            _client = new HttpClient();
            _client.BaseAddress = baseUri;
        }

        /// <summary>
        /// Creates instance of Client class, which can be used then to call public and private api methdos
        /// </summary>
        /// <param name="apiKey">api key</param>
        /// <param name="apiSecret"></param>
        /// <param name="baseUri"></param>
        private Client(string apiKey, string apiSecret, Uri baseUri)
        {
            _apiKey = apiKey;
            _apiSecret = apiSecret;

            _nonce = (ulong)DateTime.UtcNow.Ticks;

            _client = new HttpClient();
            _client.BaseAddress = baseUri;
        }
        #endregion //private constructors

        #region Factory
        /// <summary>
        /// Creates new instance of API client, which can be used to call ONLY public methods. Instance implements IDisposable, so wrap it with using statement
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public static Client CreatePublic(string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentNullException();
            }

            Uri uri;
            if (Uri.TryCreate(baseUrl, UriKind.Absolute, out uri) == false)
            {
                throw new ArgumentException("Invalid url format", "baseUrl");
            }

            return new Client(uri);
        }

        /// <summary>
        /// Creates new instance of API client, which can be used to call public and private api methods, implements IDisposable, so wrap it with using statement
        /// </summary>
        /// <param name="apiKey">your api key</param>
        /// <param name="apiSecret">your api secret</param>
        /// <param name="baseUrl">api base url, please refer to documentation for exact url depending on environment</param>
        /// <returns>instance of Client class which can be used to call public & private API methods</returns>
        public static Client CreatePrivate(string apiKey, string apiSecret, string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentNullException("apiKey");
            }

            if (string.IsNullOrWhiteSpace(apiSecret))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentNullException();
            }

            Uri uri;
            if (Uri.TryCreate(baseUrl, UriKind.Absolute, out uri) == false)
            {
                throw new ArgumentException("Invalid url format", "baseUrl");
            }

            return new Client(apiKey, apiSecret, uri);
        }
        #endregion //Factory

        #region IDisposable
        private bool _isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (_client != null)
                {
                    _client.Dispose();
                    _client = null;
                }
            }

            _isDisposed = true;
        }

        ~Client()
        {
            Dispose(false);
        }
        #endregion //IDisposable

        #region Public API
        /// <summary>
        /// Returns a list of valid primary currency codes. These are the digital currencies which can be traded on Independent Reserve
        /// </summary>
        public IEnumerable<CurrencyCode> GetValidPrimaryCurrencyCodes()
        {
            ThrowIfDisposed();
            return GetValidPrimaryCurrencyCodesAsync().Result;
        }

        /// <summary>
        /// Returns a list of valid primary currency codes. These are the digital currencies which can be traded on Independent Reserve
        /// </summary>
        public async Task<IEnumerable<CurrencyCode>> GetValidPrimaryCurrencyCodesAsync()
        {
            ThrowIfDisposed();
            return await QueryPublic<IEnumerable<CurrencyCode>>("/Public/GetValidPrimaryCurrencyCodes");
        }

        /// <summary>
        /// Returns a list of valid secondary currency codes. These are the fiat currencies which are supported by Independent Reserve for trading purposes.
        /// </summary>
        public IEnumerable<CurrencyCode> GetValidSecondaryCurrencyCodes()
        {
            ThrowIfDisposed();
            return GetValidSecondaryCurrencyCodesAsync().Result;
        }

        /// <summary>
        /// Returns a list of valid secondary currency codes. These are the fiat currencies which are supported by Independent Reserve for trading purposes.
        /// </summary>
        public async Task<IEnumerable<CurrencyCode>> GetValidSecondaryCurrencyCodesAsync()
        {
            ThrowIfDisposed();
            return await QueryPublic<IEnumerable<CurrencyCode>>("/Public/GetValidSecondaryCurrencyCodes");
        }

        /// <summary>
        /// Returns a list of valid limit order types which can be placed onto the Idependent Reserve exchange platform
        /// </summary>
        public IEnumerable<OrderType> GetValidLimitOrderTypes()
        {
            ThrowIfDisposed();
            return GetValidLimitOrderTypesAsync().Result;
        }

        /// <summary>
        /// Returns a list of valid limit order types which can be placed onto the Idependent Reserve exchange platform
        /// </summary>
        public async Task<IEnumerable<OrderType>> GetValidLimitOrderTypesAsync()
        {
            ThrowIfDisposed();
            return await QueryPublic<IEnumerable<OrderType>>("/Public/GetValidLimitOrderTypes");
        }

        /// <summary>
        /// Returns a list of valid market order types which can be placed onto the Idependent Reserve exchange platform
        /// </summary>
        public IEnumerable<OrderType> GetValidMarketOrderTypes()
        {
            ThrowIfDisposed();
            return GetValidMarketOrderTypesAsync().Result;
        }

        /// <summary>
        /// Returns a list of valid market order types which can be placed onto the Idependent Reserve exchange platform
        /// </summary>
        public async Task<IEnumerable<OrderType>> GetValidMarketOrderTypesAsync()
        {
            ThrowIfDisposed();
            return await QueryPublic<IEnumerable<OrderType>>("/Public/GetValidMarketOrderTypes");
        }

        /// <summary>
        /// Returns a current snapshot of the Independent Reserve market for a given currency pair
        /// </summary>
        /// <param name="primaryCurrency">primary currency</param>
        /// <param name="secondaryCurrency">secondary currency</param>
        public MarketSummary GetMarketSummary(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency)
        {
            ThrowIfDisposed();
            return GetMarketSummaryAsync(primaryCurrency, secondaryCurrency).Result;
        }

        /// <summary>
        /// Returns a current snapshot of the Independent Reserve market for a given currency pair
        /// </summary>
        /// <param name="primaryCurrency">primary currency</param>
        /// <param name="secondaryCurrency">secondary currency</param>
        public async Task<MarketSummary> GetMarketSummaryAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency)
        {
            ThrowIfDisposed();
            return await QueryPublic<MarketSummary>("/Public/GetMarketSummary", new Tuple<string, string>("primaryCurrencyCode", primaryCurrency.ToString()), new Tuple<string, string>("secondaryCurrencyCode",secondaryCurrency.ToString()));
        }

        /// <summary>
        /// Returns the Order Book for a given currency pair
        /// </summary>
        /// <param name="primaryCurrency">primary currency</param>
        /// <param name="secondaryCurrency">secondary currency</param>
        public OrderBook GetOrderBook(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency)
        {
            ThrowIfDisposed();
            return GetOrderBookAsync(primaryCurrency, secondaryCurrency).Result;
        }

        /// <summary>
        /// Returns the Order Book for a given currency pair
        /// </summary>
        /// <param name="primaryCurrency">primary currency</param>
        /// <param name="secondaryCurrency">secondary currency</param>
        public async Task<OrderBook> GetOrderBookAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency)
        {
            ThrowIfDisposed();
            return await QueryPublic<OrderBook>("/Public/GetOrderBook", new Tuple<string, string>("primaryCurrencyCode", primaryCurrency.ToString()), new Tuple<string, string>("secondaryCurrencyCode", secondaryCurrency.ToString()));
        }

        /// <summary>
        /// Returns summarised historical trading data for a given currency pair. Data is summarised into 1 hour intervals.
        /// </summary>
        /// <param name="primaryCurrency">primary currency</param>
        /// <param name="secondaryCurrency">secondary currency</param>
        /// <param name="numberOfHoursInThePastToRetrieve">How many past hours of historical summary data to retrieve</param>
        public TradeHistorySummary GetTradeHistorySummary(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int numberOfHoursInThePastToRetrieve)
        {
            ThrowIfDisposed();
            return GetTradeHistorySummaryAsync(primaryCurrency, secondaryCurrency, numberOfHoursInThePastToRetrieve).Result;
        }

        /// <summary>
        /// Returns summarised historical trading data for a given currency pair. Data is summarised into 1 hour intervals.
        /// </summary>
        /// <param name="primaryCurrency">primary currency</param>
        /// <param name="secondaryCurrency">secondary currency</param>
        /// <param name="numberOfHoursInThePastToRetrieve">How many past hours of historical summary data to retrieve</param>
        public async Task<TradeHistorySummary> GetTradeHistorySummaryAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int numberOfHoursInThePastToRetrieve)
        {
            ThrowIfDisposed();
            return await QueryPublic<TradeHistorySummary>("/Public/GetTradeHistorySummary"
                , new Tuple<string, string>("primaryCurrencyCode", primaryCurrency.ToString())
                , new Tuple<string, string>("secondaryCurrencyCode", secondaryCurrency.ToString())
                , new Tuple<string, string>("numberOfHoursInThePastToRetrieve", numberOfHoursInThePastToRetrieve.ToString(CultureInfo.InvariantCulture)));
        }

        /// <summary>
        /// Returns a list of most recently executed trades for a given currency pair 
        /// </summary>
        /// <param name="primaryCurrency">primary currency</param>
        /// <param name="secondaryCurrency">secondary currency</param>
        /// <param name="numberOfRecentTradesToRetrieve">how many recent trades to retrieve</param>
        public RecentTrades GetRecentTrades(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int numberOfRecentTradesToRetrieve)
        {
            ThrowIfDisposed();
            return GetRecentTradesAsync(primaryCurrency, secondaryCurrency, numberOfRecentTradesToRetrieve).Result;
        }

        /// <summary>
        /// Returns a list of most recently executed trades for a given currency pair 
        /// </summary>
        /// <param name="primaryCurrency">primary currency</param>
        /// <param name="secondaryCurrency">secondary currency</param>
        /// <param name="numberOfRecentTradesToRetrieve">how many recent trades to retrieve</param>
        public async Task<RecentTrades> GetRecentTradesAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int numberOfRecentTradesToRetrieve)
        {
            ThrowIfDisposed();
            return await QueryPublic<RecentTrades>("/Public/GetRecentTrades"
                , new Tuple<string, string>("primaryCurrencyCode", primaryCurrency.ToString())
                , new Tuple<string, string>("secondaryCurrencyCode", secondaryCurrency.ToString())
                , new Tuple<string, string>("numberOfRecentTradesToRetrieve", numberOfRecentTradesToRetrieve.ToString(CultureInfo.InvariantCulture)));
        }

        #endregion //Public API

        #region Helpers
        /// <summary>
        /// Awaitable helper method to call public api url with set of specified get parameters
        /// </summary>
        /// <typeparam name="T">type to which response should be deserialized</typeparam>
        /// <param name="url">api url (without base url part)</param>
        /// <param name="parameters">set of get parameters</param>
        /// <returns></returns>
        private async Task<T> QueryPublic<T>(string url, params Tuple<string, string>[] parameters)
        {
            //if we have get parameters - append them to the url
            if (parameters.Any())
            {
                url += "?";

                url = parameters.Aggregate(url, (current, parameter) => current + string.Format("{0}={1}&", parameter.Item1, parameter.Item2));

                url = url.TrimEnd('&');
            }

            HttpResponseMessage response = await _client.GetAsync(url);

            string result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(result);
                throw new Exception(errorMessage.Message);
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed)
            {
                throw new InvalidOperationException("Instance of Client already disposed. Create new instance.");
            }
        }
        #endregion //Helpers
    }
}
