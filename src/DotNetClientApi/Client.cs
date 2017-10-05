using System;
using System.Collections.Generic;
using System.Dynamic;
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
    public class Client : IDisposable
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

            _client = new HttpClient();
            _client.BaseAddress = baseUri;
        }

        #endregion //private constructors

        public string LastRequestUrl { get; private set; }
        public string LastRequestHttpMethod { get; private set; }
        public string LastRequestParameters { get; private set; }
        public string LastResponseRaw { get; private set; }

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
            return await QueryPublicAsync<IEnumerable<CurrencyCode>>("/Public/GetValidPrimaryCurrencyCodes").ConfigureAwait(false);
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
            return await QueryPublicAsync<IEnumerable<CurrencyCode>>("/Public/GetValidSecondaryCurrencyCodes").ConfigureAwait(false);
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
            return await QueryPublicAsync<IEnumerable<OrderType>>("/Public/GetValidLimitOrderTypes").ConfigureAwait(false);
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
            return await QueryPublicAsync<IEnumerable<OrderType>>("/Public/GetValidMarketOrderTypes").ConfigureAwait(false);
        }

        /// <summary>
        /// Returns a list of valid order types which can be placed onto the Idependent Reserve exchange platform
        /// </summary>
        public IEnumerable<OrderType> GetValidOrderTypes()
        {
            ThrowIfDisposed();
            return GetValidOrderTypesAsync().Result;
        }

        /// <summary>
        /// Returns a list of valid order types which can be placed onto the Idependent Reserve exchange platform
        /// </summary>
        public async Task<IEnumerable<OrderType>> GetValidOrderTypesAsync()
        {
            ThrowIfDisposed();
            return await QueryPublicAsync<IEnumerable<OrderType>>("/Public/GetValidOrderTypes").ConfigureAwait(false);
        }

        /// <summary>
        /// Returns a list of valid transaction types which are supported by Idependent Reserve exchange platform
        /// </summary>
        public IEnumerable<TransactionType> GetValidTransactionTypes()
        {
            ThrowIfDisposed();
            return GetValidTransactionTypesAsync().Result;
        }

        /// <summary>
        /// Returns a list of valid transaction types which are supported by Idependent Reserve exchange platform
        /// </summary>
        public async Task<IEnumerable<TransactionType>> GetValidTransactionTypesAsync()
        {
            ThrowIfDisposed();
            return await QueryPublicAsync<IEnumerable<TransactionType>>("/Public/GetValidTransactionTypes").ConfigureAwait(false);
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
            return await QueryPublicAsync<MarketSummary>("/Public/GetMarketSummary", new Tuple<string, string>("primaryCurrencyCode", primaryCurrency.ToString()), new Tuple<string, string>("secondaryCurrencyCode", secondaryCurrency.ToString())).ConfigureAwait(false);
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
            return await QueryPublicAsync<OrderBook>("/Public/GetOrderBook", new Tuple<string, string>("primaryCurrencyCode", primaryCurrency.ToString()), new Tuple<string, string>("secondaryCurrencyCode", secondaryCurrency.ToString())).ConfigureAwait(false);
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
            return await QueryPublicAsync<TradeHistorySummary>("/Public/GetTradeHistorySummary"
                , new Tuple<string, string>("primaryCurrencyCode", primaryCurrency.ToString())
                , new Tuple<string, string>("secondaryCurrencyCode", secondaryCurrency.ToString())
                , new Tuple<string, string>("numberOfHoursInThePastToRetrieve", numberOfHoursInThePastToRetrieve.ToString(CultureInfo.InvariantCulture))).ConfigureAwait(false);
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
            return await QueryPublicAsync<RecentTrades>("/Public/GetRecentTrades"
                , new Tuple<string, string>("primaryCurrencyCode", primaryCurrency.ToString())
                , new Tuple<string, string>("secondaryCurrencyCode", secondaryCurrency.ToString())
                , new Tuple<string, string>("numberOfRecentTradesToRetrieve", numberOfRecentTradesToRetrieve.ToString(CultureInfo.InvariantCulture))).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns a list of existing exchange rates between currencies.
        /// </summary>
        public IEnumerable<FxRate> GetFxRates()
        {
            ThrowIfDisposed();
            return GetFxRatesAsync().Result;
        }

        /// <summary>
        /// Returns a list of existing exchange rates between currencies.
        /// </summary>
        public async Task<IEnumerable<FxRate>> GetFxRatesAsync()
        {
            ThrowIfDisposed();
            return await QueryPublicAsync<IEnumerable<FxRate>>("/Public/GetFxRates").ConfigureAwait(false);
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

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();
            data.primaryCurrencyCode = primaryCurrency.ToString();
            data.secondaryCurrencyCode = secondaryCurrency.ToString();
            data.orderType = orderType.ToString();
            data.price = price.ToString(CultureInfo.InvariantCulture);
            data.volume = volume.ToString(CultureInfo.InvariantCulture);

            return await QueryPrivateAsync<BankOrder>("/Private/PlaceLimitOrder", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Place new market bid / offer order. A Market Bid is a buy order and a Market Offer is a sell order
        /// </summary>
        /// <param name="primaryCurrency">The digital currency code of market order</param>
        /// <param name="secondaryCurrency">The fiat currency of market order</param>
        /// <param name="orderType">The type of market order</param>
        /// <param name="volume">The volume to buy/sell in primary currency</param>
        /// <returns>newly created limit order</returns>
        public BankOrder PlaceMarketOrder(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, OrderType orderType, decimal volume)
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

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();
            data.primaryCurrencyCode = primaryCurrency.ToString();
            data.secondaryCurrencyCode = secondaryCurrency.ToString();
            data.orderType = orderType.ToString();
            data.volume = volume.ToString(CultureInfo.InvariantCulture);

            return await QueryPrivateAsync<BankOrder>("/Private/PlaceMarketOrder", data).ConfigureAwait(false);
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

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();
            data.orderGuid = orderGuid.ToString();

            return await QueryPrivateAsync<BankOrder>("/Private/CancelOrder", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves a page of a specified size, with your currently Open and Partially Filled orders
        /// </summary>
        /// <param name="primaryCurrency">The primary currency of orders</param>
        /// <param name="secondaryCurrency">The secondary currency of orders</param>
        /// <param name="pageIndex">The page index. Must be greater or equal to 1</param>
        /// <param name="pageSize">Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        /// <returns>page of a specified size, with your currently Open and Partially Filled orders</returns>
        public Page<BankHistoryOrder> GetOpenOrders(CurrencyCode? primaryCurrency, CurrencyCode? secondaryCurrency, int pageIndex, int pageSize)
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
        /// <param name="pageIndex">The page index. Must be greater or equal to 1</param>
        /// <param name="pageSize">Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        /// <returns>page of a specified size, with your currently Open and Partially Filled orders</returns>
        public async Task<Page<BankHistoryOrder>> GetOpenOrdersAsync(CurrencyCode? primaryCurrency, CurrencyCode? secondaryCurrency, int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();

            if (primaryCurrency.HasValue)
            {
                data.primaryCurrencyCode = primaryCurrency.ToString();
            }

            if (secondaryCurrency.HasValue)
            {
                data.secondaryCurrencyCode = secondaryCurrency.ToString();
            }

            data.pageIndex = pageIndex.ToString(CultureInfo.InvariantCulture);
            data.pageSize = pageSize.ToString(CultureInfo.InvariantCulture);

            return await QueryPrivateAsync<Page<BankHistoryOrder>>("/Private/GetOpenOrders", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves a page of a specified size, with your Closed and Cancelled orders
        /// </summary>
        /// <param name="primaryCurrency">The primary currency of orders</param>
        /// <param name="secondaryCurrency">The secondary currency of orders</param>
        /// <param name="pageIndex">The page index. Must be greater or equal to 1</param>
        /// <param name="pageSize">The page size. Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        /// <returns>page of a specified size, with your Closed and Cancelled orders</returns>
        public Page<BankHistoryOrder> GetClosedOrders(CurrencyCode? primaryCurrency, CurrencyCode? secondaryCurrency, int pageIndex, int pageSize)
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
        /// <param name="pageIndex">The page index. Must be greater or equal to 1</param>
        /// <param name="pageSize">The page size. Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        /// <returns>page of a specified size, with your Closed and Cancelled orders</returns>
        public async Task<Page<BankHistoryOrder>> GetClosedOrdersAsync(CurrencyCode? primaryCurrency, CurrencyCode? secondaryCurrency, int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();

            if (primaryCurrency.HasValue)
            {
                data.primaryCurrencyCode = primaryCurrency.ToString();
            }

            if (secondaryCurrency.HasValue)
            {
                data.secondaryCurrencyCode = secondaryCurrency.ToString();
            }

            data.pageIndex = pageIndex.ToString(CultureInfo.InvariantCulture);
            data.pageSize = pageSize.ToString(CultureInfo.InvariantCulture);

            return await QueryPrivateAsync<Page<BankHistoryOrder>>("/Private/GetClosedOrders", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves a page of a specified size, with your Closed filled orders
        /// </summary>
        /// <param name="primaryCurrency">The primary currency of orders</param>
        /// <param name="secondaryCurrency">The secondary currency of orders</param>
        /// <param name="pageIndex">The page index. Must be greater or equal to 1</param>
        /// <param name="pageSize">The page size. Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        /// <returns>page of a specified size, with your Closed filled orders</returns>
        public Page<BankHistoryOrder> GetClosedFilledOrders(CurrencyCode? primaryCurrency, CurrencyCode? secondaryCurrency, int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return GetClosedFilledOrdersAsync(primaryCurrency, secondaryCurrency, pageIndex, pageSize).Result;
        }

        /// <summary>
        /// Retrieves a page of a specified size, with your Closed filled orders
        /// </summary>
        /// <param name="primaryCurrency">The primary currency of orders</param>
        /// <param name="secondaryCurrency">The secondary currency of orders</param>
        /// <param name="pageIndex">The page index. Must be greater or equal to 1</param>
        /// <param name="pageSize">The page size. Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        /// <returns>page of a specified size, with your Closed filled orders</returns>
        public async Task<Page<BankHistoryOrder>> GetClosedFilledOrdersAsync(CurrencyCode? primaryCurrency, CurrencyCode? secondaryCurrency, int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();

            if (primaryCurrency.HasValue)
            {
                data.primaryCurrencyCode = primaryCurrency.ToString();
            }

            if (secondaryCurrency.HasValue)
            {
                data.secondaryCurrencyCode = secondaryCurrency.ToString();
            }

            data.pageIndex = pageIndex.ToString(CultureInfo.InvariantCulture);
            data.pageSize = pageSize.ToString(CultureInfo.InvariantCulture);

            return await QueryPrivateAsync<Page<BankHistoryOrder>>("/Private/GetClosedFilledOrders", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves order details by order Id.
        /// </summary>
        /// <param name="orderGuid">The guid of order</param>
        /// <returns>order object</returns>
        public BankOrder GetOrderDetails(Guid orderGuid)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return GetOrderDetailsAsync(orderGuid).Result;
        }

        /// <summary>
        /// Retrieves order details by order Id.
        /// </summary>
        /// <param name="orderGuid">The guid of order</param>
        /// <returns>order object</returns>
        public async Task<BankOrder> GetOrderDetailsAsync(Guid orderGuid)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();
            data.orderGuid = orderGuid.ToString();

            return await QueryPrivateAsync<BankOrder>("/Private/GetOrderDetails", data).ConfigureAwait(false);
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

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();

            return await QueryPrivateAsync<IEnumerable<Account>>("/Private/GetAccounts", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves information about user's brokerage fees
        /// </summary>
        /// <returns>a collection of brokerage fees</returns>
        public async Task<IEnumerable<BrokerageFee>> GetBrokerageFeesAsync()
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();

            return await QueryPrivateAsync<IEnumerable<BrokerageFee>>("/Private/GetBrokerageFees", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves a page of a specified size, containing all transactions made on an account
        /// </summary>
        /// <param name="accountGuid">The Guid of your Independent Reseve account. You can retrieve information about your accounts via the <see cref="GetAccounts"/> or <see cref="GetAccountsAsync"/> method</param>
        /// <param name="fromTimestampUtc">The timestamp in UTC from which you want to retrieve transactions</param>
        /// <param name="toTimestampUtc">The timestamp in UTC until which you want to retrieve transactions</param>
        /// <param name="txTypes">Transaction types array for filtering results. If array is empty or null, than no filter will be applied and all Transaction types will be returned</param>
        /// <param name="pageIndex">The page index. Must be greater or equal to 0</param>
        /// <param name="pageSize">Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        /// <returns>page of a specified size, containing all transactions made on an account</returns>
        public Page<Transaction>  GetTransactions(Guid accountGuid, DateTime? fromTimestampUtc, DateTime? toTimestampUtc, string[] txTypes, int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return GetTransactionsAsync(accountGuid, fromTimestampUtc, toTimestampUtc, txTypes, pageIndex, pageSize).Result;
        }

        /// <summary>
        /// Retrieves a page of a specified size, containing all transactions made on an account
        /// </summary>
        /// <param name="accountGuid">The Guid of your Independent Reseve account. You can retrieve information about your accounts via the <see cref="GetAccounts"/> or <see cref="GetAccountsAsync"/> method</param>
        /// <param name="fromTimestampUtc">The timestamp in UTC from which you want to retrieve transactions</param>
        /// <param name="toTimestampUtc">The timestamp in UTC until which you want to retrieve transactions</param>
        /// <param name="txTypes">Transaction types array for filtering results. If array is empty or null, than no filter will be applied and all Transaction types will be returned</param>        
        /// <param name="pageIndex">The page index. Must be greater or equal to 0</param>
        /// <param name="pageSize">Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        /// <returns>page of a specified size, containing all transactions made on an account</returns>
        public async Task<Page<Transaction>> GetTransactionsAsync(Guid accountGuid, DateTime? fromTimestampUtc,
            DateTime? toTimestampUtc, string[] txTypes, int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();
            data.accountGuid = accountGuid.ToString();
            data.fromTimestampUtc = fromTimestampUtc.HasValue ? DateTime.SpecifyKind(fromTimestampUtc.Value, DateTimeKind.Utc).ToString("u", CultureInfo.InvariantCulture) : null;
            data.toTimestampUtc = toTimestampUtc.HasValue ? DateTime.SpecifyKind(toTimestampUtc.Value, DateTimeKind.Utc).ToString("u", CultureInfo.InvariantCulture) : null;
            data.txTypes = txTypes;
            data.pageIndex = pageIndex.ToString(CultureInfo.InvariantCulture);
            data.pageSize = pageSize.ToString(CultureInfo.InvariantCulture);

            return await QueryPrivateAsync<Page<Transaction>>("/Private/GetTransactions", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves the Bitcoin address which should be used for new Bitcoin deposits
        /// </summary>
        [Obsolete("Use GetDigitalCurrencyDepositAddress instead.")]
        public BitcoinDepositAddress GetBitcoinDepositAddress()
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return GetBitcoinDepositAddressAsync().Result;
        }

        /// <summary>
        /// Retrieves the Bitcoin address which should be used for new Bitcoin deposits
        /// </summary>
        [Obsolete("Use GetDigitalCurrencyDepositAddressAsync instead.")]
        public async Task<BitcoinDepositAddress> GetBitcoinDepositAddressAsync()
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();

            return await QueryPrivateAsync<BitcoinDepositAddress>("/Private/GetBitcoinDepositAddress", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves the deposit address which should be used for new digital currency deposits
        /// </summary>
        /// <param name="primaryCurrency">digital currency code to generate deposit address for</param>
        public DigitalCurrencyDepositAddress GetDigitalCurrencyDepositAddress(CurrencyCode primaryCurrency)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return GetDigitalCurrencyDepositAddressAsync(primaryCurrency).Result;
        }

        /// <summary>
        /// Retrieves the Bitcoin address which should be used for new Bitcoin deposits
        /// </summary>
        /// <param name="primaryCurrency">digital currency code to generate deposit address for</param>
        public async Task<DigitalCurrencyDepositAddress> GetDigitalCurrencyDepositAddressAsync(CurrencyCode primaryCurrency)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();
            data.primaryCurrencyCode = primaryCurrency.ToString();

            return await QueryPrivateAsync<DigitalCurrencyDepositAddress>("/Private/GetDigitalCurrencyDepositAddress", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves the Bitcoin addresses (paged) which should be used for new Bitcoin deposits
        /// </summary>
        [Obsolete("Use GetDigitalCurrencyDepositAddresses instead.")]
        public Page<BitcoinDepositAddress> GetBitcoinDepositAddresses(int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return GetBitcoinDepositAddressesAsync(pageIndex, pageSize).Result;
        }

        /// <summary>
        /// Retrieves the Bitcoin addresses (paged) which should be used for new Bitcoin deposits
        /// </summary>
        [Obsolete("Use GetDigitalCurrencyDepositAddressesAsync instead.")]
        public async Task<Page<BitcoinDepositAddress>> GetBitcoinDepositAddressesAsync(int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();
            data.pageIndex = pageIndex;
            data.pageSize = pageSize;

            return await QueryPrivateAsync<Page<BitcoinDepositAddress>>("/Private/GetBitcoinDepositAddresses", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves deposit addresses (paged) which should be used for new Bitcoin or Ether deposits
        /// </summary>
        /// <param name="primaryCurrency">digital currency code to retrieve deposit addresses for</param>
        /// <param name="pageIndex">The page index. Must be greater or equal to 1</param>
        /// <param name="pageSize">Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        public Page<DigitalCurrencyDepositAddress> GetDigitalCurrencyDepositAddresses(CurrencyCode primaryCurrency, int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return GetDigitalCurrencyDepositAddressesAsync(primaryCurrency, pageIndex, pageSize).Result;
        }

        /// <summary>
        /// Retrieves the Bitcoin addresses (paged) which should be used for new Bitcoin deposits
        /// </summary>
        /// <param name="primaryCurrency">digital currency code to retrieve deposit addresses for</param>
        /// <param name="pageIndex">The page index. Must be greater or equal to 1</param>
        /// <param name="pageSize">Must be greater or equal to 1 and less than or equal to 50. If a number greater than 50 is specified, then 50 will be used</param>
        public async Task<Page<DigitalCurrencyDepositAddress>> GetDigitalCurrencyDepositAddressesAsync(CurrencyCode primaryCurrency, int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();
            data.primaryCurrencyCode = primaryCurrency.ToString();
            data.pageIndex = pageIndex;
            data.pageSize = pageSize;

            return await QueryPrivateAsync<Page<DigitalCurrencyDepositAddress>>("/Private/GetDigitalCurrencyDepositAddresses", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Marks bitcoin address to sync with blockchain and update balance
        /// </summary>
        /// <param name="bitcoinAddress">Bitcoin address</param>
        /// <returns>A BitcoinDepositAddress object</returns>
        [Obsolete("Use SynchDigitalCurrencyDepositAddressWithBlockchain instead.")]
        public BitcoinDepositAddress SynchBitcoinAddressWithBlockchain(string bitcoinAddress)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return SynchBitcoinAddressWithBlockchainAsync(bitcoinAddress).Result;
        }

        /// <summary>
        /// Marks bitcoin address to sync with blockchain and update balance
        /// </summary>
        /// <param name="bitcoinAddress">Bitcoin address</param>
        /// <returns>A BitcoinDepositAddress object</returns>
        [Obsolete("Use SynchDigitalCurrencyDepositAddressWithBlockchainAsync instead.")]
        public async Task<BitcoinDepositAddress> SynchBitcoinAddressWithBlockchainAsync(string bitcoinAddress)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();
            data.bitcoinAddress = bitcoinAddress;

            return await QueryPrivateAsync<BitcoinDepositAddress>("/Private/SynchBitcoinAddressWithBlockchain", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Marks digital currency deposit address to sync with blockchain and update balance
        /// </summary>
        /// <param name="depositAddress">Digital currency deposit address to sync</param>
        /// <param name="primaryCurrency">Optional primary currency</param>
        /// <returns>A DigitalCurrnecyDepositAddress object</returns>
        public DigitalCurrencyDepositAddress SynchDigitalCurrencyDepositAddressWithBlockchain(string depositAddress, CurrencyCode? primaryCurrency = null)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return SynchDigitalCurrencyDepositAddressWithBlockchainAsync(depositAddress, primaryCurrency).Result;
        }

        /// <summary>
        /// Marks digital currency deposit address to sync with blockchain and update balance
        /// </summary>
        /// <param name="depositAddress">Digital currency deposit address to sync</param>
        /// <param name="primaryCurrency">Optional primary currency</param>
        /// <returns>A DigitalCurrencyDepositAddress object</returns>
        public async Task<DigitalCurrencyDepositAddress> SynchDigitalCurrencyDepositAddressWithBlockchainAsync(string depositAddress, CurrencyCode? primaryCurrency = null)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();
            data.depositAddress = depositAddress;
            if (primaryCurrency.HasValue)
            {
                data.primaryCurrencyCode = primaryCurrency.ToString();
            }

            return await QueryPrivateAsync<DigitalCurrencyDepositAddress>("/Private/SynchDigitalCurrencyDepositAddressWithBlockchain", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates bitcoin withdrawal request
        /// </summary>
        /// <param name="withdrawalAmount">withdrawal amount</param>
        /// <param name="bitcoinAddress">bitcoin address to withdraw</param>
        /// <param name="comment">withdrawal comment</param>
        [Obsolete("Use WithdrawDigitalCurrency instead.")]
        public void WithdrawBitcoin(decimal? withdrawalAmount, string bitcoinAddress, string comment)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            WithdrawBitcoinAsync(withdrawalAmount, bitcoinAddress, comment).Wait();
        }

        /// <summary>
        /// Creates bitcoin withdrawal request
        /// </summary>
        /// <param name="withdrawalAmount">withdrawal amount</param>
        /// <param name="bitcoinAddress">bitcoin address to withdraw</param>
        /// <param name="comment">withdrawal comment</param>
        [Obsolete("Use WithdrawDigitalCurrencyAsync instead.")]
        public async Task WithdrawBitcoinAsync(decimal? withdrawalAmount, string bitcoinAddress, string comment)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();
            data.amount = withdrawalAmount.HasValue ? withdrawalAmount.Value.ToString(CultureInfo.InvariantCulture) : null;
            data.bitcoinAddress = bitcoinAddress;
            data.comment = comment;

            await QueryPrivateAsync("/Private/WithdrawBitcoin", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates digital currency withdrawal request
        /// </summary>
        /// <param name="withdrawalAmount">withdrawal amount</param>
        /// <param name="withdrawalAddress">digital address to withdraw</param>
        /// <param name="comment">withdrawal comment</param>
        /// <param name="primaryCurrency">optional primary currency</param>
        public void WithdrawDigitalCurrency(decimal withdrawalAmount, string withdrawalAddress, string comment, CurrencyCode? primaryCurrency = null)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            WithdrawDigitalCurrencyAsync(withdrawalAmount, withdrawalAddress, comment, primaryCurrency).Wait();
        }

        /// <summary>
        /// Creates digital currency withdrawal request
        /// </summary>
        /// <param name="withdrawalAmount">withdrawal amount</param>
        /// <param name="withdrawalAddress">digital address to withdraw</param>
        /// <param name="comment">withdrawal comment</param>
        /// <param name="primaryCurrency">optional primary currency</param>
        public async Task WithdrawDigitalCurrencyAsync(decimal withdrawalAmount, string withdrawalAddress, string comment, CurrencyCode? primaryCurrency = null)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();
            data.amount = withdrawalAmount.ToString(CultureInfo.InvariantCulture);
            data.withdrawalAddress = withdrawalAddress;
            data.comment = comment;
            if (primaryCurrency.HasValue)
            {
                data.primaryCurrencyCode = primaryCurrency.ToString();
            }

            await QueryPrivateAsync("/Private/WithdrawDigitalCurrency", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a withdrawal request for a Fiat currency withdrawal from your Independent Reserve account to an external bank account
        /// </summary>
        /// <param name="secondaryCurrency">The Independent Reserve fiat currency account to withdraw from (currently only USD accounts are supported)</param>
        /// <param name="withdrawalAmount">Amount of fiat currency to withdraw</param>
        /// <param name="withdrawalBankAccountName">A pre-configured bank account you've already linked to your Independent Reserve account</param>
        /// <param name="comment">withdrawal comment</param>
        /// <returns>A FiatWithdrawalRequest object</returns>
        public FiatWithdrawalRequest RequestFiatWithdrawal(CurrencyCode secondaryCurrency, decimal withdrawalAmount, string withdrawalBankAccountName, string comment)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return RequestFiatWithdrawalAsync(secondaryCurrency, withdrawalAmount, withdrawalBankAccountName, comment).Result;
        }

        /// <summary>
        /// Creates a withdrawal request for a Fiat currency withdrawal from your Independent Reserve account to an external bank account
        /// </summary>
        /// <param name="secondaryCurrency">The Independent Reserve fiat currency account to withdraw from (currently only USD accounts are supported)</param>
        /// <param name="withdrawalAmount">Amount of fiat currency to withdraw</param>
        /// <param name="withdrawalBankAccountName">A pre-configured bank account you've already linked to your Independent Reserve account</param>
        /// <param name="comment">withdrawal comment</param>
        /// <returns>A FiatWithdrawalRequest object</returns>
        public async Task<FiatWithdrawalRequest> RequestFiatWithdrawalAsync(CurrencyCode secondaryCurrency, decimal withdrawalAmount, string withdrawalBankAccountName, string comment)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();
            data.secondaryCurrencyCode = secondaryCurrency.ToString();
            data.withdrawalAmount = withdrawalAmount.ToString(CultureInfo.InvariantCulture);
            data.withdrawalBankAccountName = withdrawalBankAccountName;
            data.comment = comment;

            return await QueryPrivateAsync<FiatWithdrawalRequest>("/Private/RequestFiatWithdrawal", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves recent trades made by user.
        /// </summary>
        /// <param name="pageIndex">1 based page index</param>
        /// <param name="pageSize">page size must be greater or equal 1 and not exceed 50</param>
        /// <returns>a page of a specified size containing recent trades mady by user</returns>
        public Page<TradeDetails> GetTrades(int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return GetTradesAsync(pageIndex, pageSize).Result;
        }

        /// <summary>
        /// Retrieves recent trades made by user.
        /// </summary>
        /// <param name="pageIndex">1 based page index</param>
        /// <param name="pageSize">page size must be greater or equal 1 and not exceed 50</param>
        /// <returns>a page of a specified size containing recent trades mady by user</returns>
        public async Task<Page<TradeDetails>> GetTradesAsync(int pageIndex, int pageSize)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();
            data.pageIndex = pageIndex;
            data.pageSize = pageSize;

            return await QueryPrivateAsync<Page<TradeDetails>>("/Private/GetTrades", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves information about user's brokerage fees
        /// </summary>
        /// <returns>a collection of brokerage fees</returns>
        public IEnumerable<BrokerageFee> GetBrokerageFees()
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            return GetBrokerageFeesAsync().Result;
        }

        #endregion //Private API

        #region Helpers

        /// <summary>
        /// Awaitable helper method to call public api url with set of specified get parameters
        /// </summary>
        /// <typeparam name="T">type to which response should be deserialized</typeparam>
        /// <param name="url">api url (without base url part)</param>
        /// <param name="parameters">set of get parameters</param>
        private async Task<T> QueryPublicAsync<T>(string url, params Tuple<string, string>[] parameters)
        {
            LastRequestParameters = string.Empty;
            LastRequestUrl = url;
            LastRequestHttpMethod = "GET";

            //if we have get parameters - append them to the url
            if (parameters.Any())
            {
                string queryString = parameters.Aggregate(string.Empty, (current, parameter) => current + string.Format("{0}={1}&", parameter.Item1, parameter.Item2)).TrimEnd('&');

                LastRequestParameters = queryString;

                url = string.Format("{0}?{1}", url, queryString);
            }

            HttpResponseMessage response = await _client.GetAsync(url).ConfigureAwait(false);

            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            LastResponseRaw = result;

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
        private async Task<T> QueryPrivateAsync<T>(string url, dynamic request)
        {
            HttpContent content = CreateRequestContent(url, request);

            HttpResponseMessage response = await _client.PostAsync(url, content).ConfigureAwait(false);

            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            LastResponseRaw = result;

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
        /// <param name="url">api url (without base url part)</param>
        /// <param name="request">object to post</param>
        private async Task QueryPrivateAsync(string url, dynamic request)
        {
            HttpContent content = CreateRequestContent(url, request);

            HttpResponseMessage response = await _client.PostAsync(url, content).ConfigureAwait(false);

            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            LastResponseRaw = result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(result);
                throw new Exception(errorMessage.Message);
            }
        }

        /// <summary>
        /// Prepares post request content.
        /// </summary>
        private HttpContent CreateRequestContent(string url, dynamic request)
        {
            // Calculate signature against all request parameters
            request.signature = GetSignature(url, request as IDictionary<string, object>);

            string parameters = JsonConvert.SerializeObject(request);

            LastRequestParameters = parameters;
            LastRequestUrl = url;
            LastRequestHttpMethod = "POST";

            return new StringContent(parameters, Encoding.UTF8, "application/json");
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
        /// Helper method to get nonce.
        /// </summary>
        /// <returns></returns>
        private string GetNonce()
        {
            return DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Helper method to get signature which can be used to 'sign' private api request.
        /// </summary>
        private string GetSignature(string url, IDictionary<String, Object> requestParameters)
        {
            var input = new StringBuilder(new Uri(_client.BaseAddress, url).ToString());

            foreach (string key in requestParameters.Keys)
            {
                input.Append(',');
                var value = requestParameters[key];
                object resultValue;

                if (value != null && value.GetType().IsArray && typeof (string).IsAssignableFrom(value.GetType().GetElementType()))
                {
                    var array = value as string[];
                    resultValue = array == null || !array.Any() ? string.Empty : string.Join(",", array.Select(tt => string.Format("{0}", tt)));
                }
                else
                {
                    if (value is string)
                    {
                        resultValue = ((string)value).Replace("\r", "").Replace("\n", "");
                    }
                    else
                    {
                        resultValue = value;
                    }
                }

                input.AppendFormat(CultureInfo.InvariantCulture, "{0}={1}", key, resultValue);
            }

            return HMACSHA256Hash(input.ToString(), _apiSecret);
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
                if (ch <= 0x7f) retval[ix] = (byte) ch;
                else retval[ix] = (byte) '?';
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