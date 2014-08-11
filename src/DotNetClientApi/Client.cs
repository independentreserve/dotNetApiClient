using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IndependentReserve.DotNetClientApi.Data;
using Newtonsoft.Json;

namespace IndependentReserve.DotNetClientApi
{
    /// <summary>
    /// IndependentReserve API client, implements IDisposable
    /// </summary>
    public class Client:IDisposable
    {
        private readonly string _apiKey;
        private readonly string _apiSecret;

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

        #region Private API

        /// <summary>
        /// Places new limit bid / offer order. A Limit Bid is a buy order and a Limit Offer is a sell order
        /// </summary>
        /// <param name="primaryCurrency">The digital currency code of limit order</param>
        /// <param name="secondaryCurrency">The fiat currency of limit order</param>
        /// <param name="orderType">The type of limit order</param>
        /// <param name="price">The price in secondary currency to buy/sell</param>
        /// <param name="volume">The volume to buy/sell in primary currency</param>
        /// <returns>newly created limit order</returns>
        public BankOrder PlaceLimitOrder(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, OrderType orderType, decimal price, decimal volume)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();
            return PlaceLimitOrderAsync(primaryCurrency, secondaryCurrency, orderType, price, volume).Result;
        }

        /// <summary>
        /// Places new limit bid / offer order. A Limit Bid is a buy order and a Limit Offer is a sell order
        /// </summary>
        /// <param name="primaryCurrency">The digital currency code of limit order</param>
        /// <param name="secondaryCurrency">The fiat currency of limit order</param>
        /// <param name="orderType">The type of limit order</param>
        /// <param name="price">The price in secondary currency to buy/sell</param>
        /// <param name="volume">The volume to buy/sell in primary currency</param>
        /// <returns>newly created limit order</returns>
        public async Task<BankOrder> PlaceLimitOrderAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, OrderType orderType, decimal price, decimal volume)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            var nonceAndSignature = GetNonceAndSignature();

            return await QueryPrivate<BankOrder>("/Private/PlaceLimitOrder", new
            {
                apiKey = _apiKey,
                nonce = nonceAndSignature.Item1,
                signature = nonceAndSignature.Item2,
                primaryCurrencyCode = primaryCurrency.ToString(),
                secondaryCurrencyCode = secondaryCurrency.ToString(),
                orderType=orderType.ToString(),
                price=price,
                volume=volume
            });
        }

        /// <summary>
        /// Place new market bid / offer order. A Market Bid is a buy order and a Market Offer is a sell order
        /// </summary>
        /// <param name="primaryCurrency">The digital currency code of market order</param>
        /// <param name="secondaryCurrency">The fiat currency of market order</param>
        /// <param name="orderType">The type of market order</param>
        /// <param name="volume">The volume to buy/sell in primary currency</param>
        /// <returns>newly created limit order</returns>
        public BankOrder PlaceMarketOrder(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency,
            OrderType orderType, decimal volume)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return PlaceMarketOrderAsync(primaryCurrency, secondaryCurrency, orderType, volume).Result;
        }

        /// <summary>
        /// Place new market bid / offer order. A Market Bid is a buy order and a Market Offer is a sell order
        /// </summary>
        /// <param name="primaryCurrency">The digital currency code of market order</param>
        /// <param name="secondaryCurrency">The fiat currency of market order</param>
        /// <param name="orderType">The type of market order</param>
        /// <param name="volume">The volume to buy/sell in primary currency</param>
        /// <returns>newly created limit order</returns>
        public async Task<BankOrder> PlaceMarketOrderAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, OrderType orderType, decimal volume)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            var nonceAndSignature = GetNonceAndSignature();

            return await QueryPrivate<BankOrder>("/Private/PlaceMarketOrder", new
            {
                apiKey = _apiKey,
                nonce = nonceAndSignature.Item1,
                signature = nonceAndSignature.Item2,
                primaryCurrencyCode = primaryCurrency.ToString(),
                secondaryCurrencyCode = secondaryCurrency.ToString(),
                orderType = orderType.ToString(),
                volume = volume
            });
        }

        /// <summary>
        /// Cancels a previously placed order
        /// </summary>
        /// <param name="orderGuid">The guid of currently open or partially filled order</param>
        /// <returns>cancelled order</returns>
        /// <remarks>
        /// The order must be in either 'Open' or 'PartiallyFilled' status to be valid for cancellation. You can retrieve list of Open and Partially Filled orders via the <see cref="GetOpenOrdersAsync"/> or <see cref="GetOpenOrders"/>  methods.
        /// </remarks>
        public BankOrder CancelOrder(Guid orderGuid)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return CancelOrderAsync(orderGuid).Result;
        }

        /// <summary>
        /// Cancels a previously placed order
        /// </summary>
        /// <param name="orderGuid">The guid of currently open or partially filled order</param>
        /// <returns>cancelled order</returns>
        /// <remarks>
        /// The order must be in either 'Open' or 'PartiallyFilled' status to be valid for cancellation. You can retrieve list of Open and Partially Filled orders via the <see cref="GetOpenOrdersAsync"/> or <see cref="GetOpenOrders"/>  methods.
        /// </remarks>
        public async Task<BankOrder> CancelOrderAsync(Guid orderGuid)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            var nonceAndSignature = GetNonceAndSignature();

            return await QueryPrivate<BankOrder>("/Private/CancelOrder", new
            {
                apiKey = _apiKey,
                nonce = nonceAndSignature.Item1,
                signature = nonceAndSignature.Item2,
                orderGuid = orderGuid.ToString()
            });
        }

        /// <summary>
        /// Retrieves a page of a specified size, with your currently Open and Partially Filled orders
        /// </summary>
        /// <param name="primaryCurrency">The primary currency of orders</param>
        /// <param name="secondaryCurrency">The secondary currency of orders</param>
        /// <param name="pageIndex">The page index. Must be greater or equal to 0</param>
        /// <param name="pageSize">Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        /// <returns>page of a specified size, with your currently Open and Partially Filled orders</returns>
        public Page<BankHistoryOrder> GetOpenOrders(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return GetOpenOrdersAsync(primaryCurrency, secondaryCurrency, pageIndex, pageSize).Result;
        }

        /// <summary>
        /// Retrieves a page of a specified size, with your currently Open and Partially Filled orders
        /// </summary>
        /// <param name="primaryCurrency">The primary currency of orders</param>
        /// <param name="secondaryCurrency">The secondary currency of orders</param>
        /// <param name="pageIndex">The page index. Must be greater or equal to 0</param>
        /// <param name="pageSize">Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        /// <returns>page of a specified size, with your currently Open and Partially Filled orders</returns>
        public async Task<Page<BankHistoryOrder>> GetOpenOrdersAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            var nonceAndSignature = GetNonceAndSignature();

            return await QueryPrivate<Page<BankHistoryOrder>>("/Private/GetOpenOrders", new
            {
                apiKey = _apiKey,
                nonce = nonceAndSignature.Item1,
                signature = nonceAndSignature.Item2,
                primaryCurrencyCode = primaryCurrency.ToString(),
                secondaryCurrencyCode = secondaryCurrency.ToString(),
                pageIndex = pageIndex,
                pageSize = pageSize
            });
        }

        /// <summary>
        /// Retrieves a page of a specified size, with your Closed and Cancelled orders
        /// </summary>
        /// <param name="primaryCurrency">The primary currency of orders</param>
        /// <param name="secondaryCurrency">The secondary currency of orders</param>
        /// <param name="pageIndex">The page index. Must be greater or equal to 0</param>
        /// <param name="pageSize">The page size. Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        /// <returns>page of a specified size, with your Closed and Cancelled orders</returns>
        public Page<BankHistoryOrder> GetClosedOrders(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return GetClosedOrdersAsync(primaryCurrency, secondaryCurrency, pageIndex, pageSize).Result;
        }

        /// <summary>
        /// Retrieves a page of a specified size, with your Closed and Cancelled orders
        /// </summary>
        /// <param name="primaryCurrency">The primary currency of orders</param>
        /// <param name="secondaryCurrency">The secondary currency of orders</param>
        /// <param name="pageIndex">The page index. Must be greater or equal to 0</param>
        /// <param name="pageSize">The page size. Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        /// <returns>page of a specified size, with your Closed and Cancelled orders</returns>
        public async Task<Page<BankHistoryOrder>> GetClosedOrdersAsync(CurrencyCode primaryCurrency,CurrencyCode secondaryCurrency, int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            var nonceAndSignature = GetNonceAndSignature();

            return await QueryPrivate<Page<BankHistoryOrder>>("/Private/GetClosedOrders", new
            {
                apiKey = _apiKey,
                nonce = nonceAndSignature.Item1,
                signature = nonceAndSignature.Item2,
                primaryCurrencyCode = primaryCurrency.ToString(),
                secondaryCurrencyCode = secondaryCurrency.ToString(),
                pageIndex = pageIndex,
                pageSize = pageSize
            });
        }

        /// <summary>
        /// Retrieves information about your Independent Reserve accounts in digital and fiat currencies
        /// </summary>
        public IEnumerable<Account> GetAccounts()
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return GetAccountsAsync().Result;
        }

        /// <summary>
        /// Retrieves information about your Independent Reserve accounts in digital and fiat currencies
        /// </summary>
        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            var nonceAndSignature = GetNonceAndSignature();

            return await QueryPrivate<IEnumerable<Account>>("/Private/GetAccounts", new
            {
                apiKey = _apiKey,
                nonce = nonceAndSignature.Item1,
                signature = nonceAndSignature.Item2,
            });
        }

        /// <summary>
        /// Retrieves a page of a specified size, containing all transactions made on an account
        /// </summary>
        /// <param name="accountGuid">The Guid of your Independent Reseve account. You can retrieve information about your accounts via the <see cref="GetAccounts"/> or <see cref="GetAccountsAsync"/> method</param>
        /// <param name="fromTimestampUtc">The timestamp in UTC from which you want to retrieve transactions</param>
        /// <param name="toTimestampUtc">The timestamp in UTC until which you want to retrieve transactions</param>
        /// <param name="pageIndex">The page index. Must be greater or equal to 0</param>
        /// <param name="pageSize">Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        /// <returns>page of a specified size, containing all transactions made on an account</returns>
        public Page<Transaction>  GetTransactions(Guid accountGuid, DateTime? fromTimestampUtc, DateTime? toTimestampUtc, int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return GetTransactionsAsync(accountGuid, fromTimestampUtc, toTimestampUtc, pageIndex, pageSize).Result;
        }

        /// <summary>
        /// Retrieves a page of a specified size, containing all transactions made on an account
        /// </summary>
        /// <param name="accountGuid">The Guid of your Independent Reseve account. You can retrieve information about your accounts via the <see cref="GetAccounts"/> or <see cref="GetAccountsAsync"/> method</param>
        /// <param name="fromTimestampUtc">The timestamp in UTC from which you want to retrieve transactions</param>
        /// <param name="toTimestampUtc">The timestamp in UTC until which you want to retrieve transactions</param>
        /// <param name="pageIndex">The page index. Must be greater or equal to 0</param>
        /// <param name="pageSize">Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        /// <returns>page of a specified size, containing all transactions made on an account</returns>
        public async Task<Page<Transaction>> GetTransactionsAsync(Guid accountGuid, DateTime? fromTimestampUtc,
            DateTime? toTimestampUtc, int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            var nonceAndSignature = GetNonceAndSignature();

            return await QueryPrivate<Page<Transaction>>("/Private/GetTransactions", new
            {
                apiKey = _apiKey,
                nonce = nonceAndSignature.Item1,
                signature = nonceAndSignature.Item2,
                accountGuid = accountGuid.ToString(),
                fromTimestampUtc = fromTimestampUtc.HasValue? DateTime.SpecifyKind(fromTimestampUtc.Value,DateTimeKind.Utc):(DateTime?)null,
                toTimestampUtc = toTimestampUtc.HasValue ? DateTime.SpecifyKind(toTimestampUtc.Value, DateTimeKind.Utc) : (DateTime?)null,
                pageIndex,
                pageSize
            });
        }

        /// <summary>
        /// Retrieves the Bitcoin address which should be used for new Bitcoin deposits
        /// </summary>
        public BitcoinDepositAddress GetBitcoinDepositAddress()
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return GetBitcoinDepositAddressAsync().Result;
        }

        /// <summary>
        /// Retrieves the Bitcoin address which should be used for new Bitcoin deposits
        /// </summary>
        public async Task<BitcoinDepositAddress> GetBitcoinDepositAddressAsync()
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            var nonceAndSignature = GetNonceAndSignature();

            return await QueryPrivate<BitcoinDepositAddress>("/Private/GetBitcoinDepositAddress", new
            {
                apiKey = _apiKey,
                nonce = nonceAndSignature.Item1,
                signature = nonceAndSignature.Item2,
            });
        }

        #endregion //Private API

        #region Helpers
        
        /// <summary>
        /// Awaitable helper method to call public api url with set of specified get parameters
        /// </summary>
        /// <typeparam name="T">type to which response should be deserialized</typeparam>
        /// <param name="url">api url (without base url part)</param>
        /// <param name="parameters">set of get parameters</param>
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

        /// <summary>
        /// Awaitable helper method to call private api url posting specified request object as json content
        /// </summary>
        /// <typeparam name="T">type to which response should be deserialized</typeparam>
        /// <param name="url">api url (without base url part)</param>
        /// <param name="request">object to post</param>
        private async Task<T> QueryPrivate<T>(string url, object request)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(url, content);

            string result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(result);
                throw new Exception(errorMessage.Message);
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// Throws InvalidOperationException if client is disposed
        /// </summary>
        private void ThrowIfDisposed()
        {
            if (_isDisposed)
            {
                throw new InvalidOperationException("Instance of Client already disposed. Create new instance.");
            }
        }

        /// <summary>
        /// Throws InvalidOperationException if client is not configured to call private api
        /// </summary>
        private void ThrowIfPublicClient()
        {
            if (string.IsNullOrWhiteSpace(_apiKey) || string.IsNullOrWhiteSpace(_apiSecret))
            {
                throw new InvalidOperationException("This instance of Client can access Public API only. Use CreatePrivate method to create instance of client to call Private API.");
            }
        }

        /// <summary>
        /// Helper method to get signature which can be used to 'sign' private api request
        /// </summary>
        /// <returns></returns>
        private Tuple<string,string> GetNonceAndSignature()
        {
            string nonce = DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture);
            var tuple = new Tuple<string, string>(nonce,
                HMACSHA256Hash(string.Format("{0}{1}", nonce, _apiKey), _apiSecret));

            return tuple;
        }

        /// <summary>
        /// Calculates HMACSHA256 hash of specified message with specified key
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string HMACSHA256Hash(string message, string key)
        {
            byte[] keyBytes = StringToAscii(key);
            byte[] messageBytes = StringToAscii(message);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                return BitConverter.ToString(hmac.ComputeHash(messageBytes)).Replace("-", "");
            }
        }

        /// <summary>
        /// Helper method to convert string into byte array uasing ASCII encoding
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static byte[] StringToAscii(string s)
        {
            var retval = new byte[s.Length];
            for (int ix = 0; ix < s.Length; ++ix)
            {
                char ch = s[ix];
                if (ch <= 0x7f) retval[ix] = (byte)ch;
                else retval[ix] = (byte)'?';
            }
            return retval;
        }
        #endregion //Helpers

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
    }
}
