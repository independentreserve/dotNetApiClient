using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Threading.Tasks;
using IndependentReserve.DotNetClientApi.Data;
using IndependentReserve.DotNetClientApi.Data.Limits;

namespace IndependentReserve.DotNetClientApi
{


    /// <summary>
    /// IndependentReserve API client, implements IDisposable
    /// </summary>
    public class Client : IDisposable , IClient
    {
        private readonly string _apiKey;

        internal IHttpWorker HttpWorker { get; set; }


        public string LastRequestUrl => HttpWorker.LastRequestUrl;

        public string LastRequestHttpMethod => HttpWorker.LastRequestHttpMethod;
        public string LastRequestParameters => HttpWorker.LastRequestParameters;
        public string LastResponseRaw => HttpWorker.LastResponseRaw;

        /// <summary>
        /// Support injecting an alternate implementation
        /// </summary>
        public static Func<string> GetNonceProvider = () => DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);

        #region private constructors

        /// <summary>
        /// Creates instance of Client class, which can be used then to call public ONLY api methdos
        /// </summary>
        private Client(Uri baseUri)
        {
            HttpWorker = new HttpWorker(baseUri);
        }

        /// <summary>
        /// Creates instance of Client class, which can be used then to call public and private api methdos
        /// </summary>
        private Client(string apiKey, string apiSecret, Uri baseUri) : this(baseUri)
        {
            _apiKey = apiKey;
            HttpWorker.ApiSecret = apiSecret;
        }

        #endregion //private constructors

        #region Factory

        /// <summary>
        /// Creates new instance of API client, which can be used to call ONLY public methods. Instance implements IDisposable, so wrap it with using statement
        /// </summary>
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
        /// Create client instance for public or private depending on whether credentials supplied
        /// </summary>
        public static Client Create(ApiConfig config)
        {
            if (config.HasCredential)
            {
                return CreatePrivate(config.Credential.Key, config.Credential.Secret, config.BaseUrl);
            }
            else
            {
                return CreatePublic(config.BaseUrl);
            }
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
        /// <remarks>
        /// example endpoint: https://api.independentreserve.com/Public/GetValidPrimaryCurrencyCodes
        /// </remarks>
        public async Task<IEnumerable<CurrencyCode>> GetValidPrimaryCurrencyCodesAsync()
        {
            ThrowIfDisposed();
            var currencyCodeStrings = await HttpWorker.QueryPublicAsync<string[]>("/Public/GetValidPrimaryCurrencyCodes").ConfigureAwait(false);

            var currencyCodeList = new List<CurrencyCode>();
            foreach(var code in currencyCodeStrings)
            {
                CurrencyCode enumCode;
                if (Enum.TryParse(code, out enumCode))
                {
                    currencyCodeList.Add(enumCode);
                }
            }

            return currencyCodeList;
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
            return await HttpWorker.QueryPublicAsync<IEnumerable<CurrencyCode>>("/Public/GetValidSecondaryCurrencyCodes").ConfigureAwait(false);
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
            return await HttpWorker.QueryPublicAsync<IEnumerable<OrderType>>("/Public/GetValidLimitOrderTypes").ConfigureAwait(false);
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
            return await HttpWorker.QueryPublicAsync<IEnumerable<OrderType>>("/Public/GetValidMarketOrderTypes").ConfigureAwait(false);
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
            return await HttpWorker.QueryPublicAsync<IEnumerable<OrderType>>("/Public/GetValidOrderTypes").ConfigureAwait(false);
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
            return await HttpWorker.QueryPublicAsync<IEnumerable<TransactionType>>("/Public/GetValidTransactionTypes").ConfigureAwait(false);
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
            return await HttpWorker.QueryPublicAsync<MarketSummary>("/Public/GetMarketSummary", new Tuple<string, string>("primaryCurrencyCode", primaryCurrency.ToString()), new Tuple<string, string>("secondaryCurrencyCode", secondaryCurrency.ToString())).ConfigureAwait(false);
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
            return await HttpWorker.QueryPublicAsync<OrderBook>("/Public/GetOrderBook", new Tuple<string, string>("primaryCurrencyCode", primaryCurrency.ToString()), new Tuple<string, string>("secondaryCurrencyCode", secondaryCurrency.ToString())).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the Order Book for a given currency pair including orders identifiers
        /// </summary>
        /// <param name="primaryCurrency">primary currency</param>
        /// <param name="secondaryCurrency">secondary currency</param>
        public OrderBookDetailed GetAllOrders(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency)
        {
            ThrowIfDisposed();
            return GetAllOrdersAsync(primaryCurrency, secondaryCurrency).Result;
        }

        /// <summary>
        /// Returns the Order Book for a given currency pair including orders identifiers
        /// </summary>
        /// <param name="primaryCurrency">primary currency</param>
        /// <param name="secondaryCurrency">secondary currency</param>
        public async Task<OrderBookDetailed> GetAllOrdersAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency)
        {
            ThrowIfDisposed();
            return await HttpWorker.QueryPublicAsync<OrderBookDetailed>("/Public/GetAllOrders", new Tuple<string, string>("primaryCurrencyCode", primaryCurrency.ToString()), new Tuple<string, string>("secondaryCurrencyCode", secondaryCurrency.ToString())).ConfigureAwait(false);
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
            return await HttpWorker.QueryPublicAsync<TradeHistorySummary>("/Public/GetTradeHistorySummary"
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
            return await HttpWorker.QueryPublicAsync<RecentTrades>("/Public/GetRecentTrades"
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
            return await HttpWorker.QueryPublicAsync<IEnumerable<FxRate>>("/Public/GetFxRates").ConfigureAwait(false);
        }

        public async Task<List<Event>> GetEvents()
        {
            ThrowIfDisposed();
            return await HttpWorker.QueryPublicAsync<List<Event>>("/Public/Events");
        }

        /// <summary>
        /// Returns exchange status for all currency codes
        /// </summary>
        public async Task<ExchangeStatus> GetExchangeStatus()
        {
            ThrowIfDisposed();
            return await HttpWorker.QueryPublicAsync<ExchangeStatus>("/Public/GetExchangeStatus");
        }

        /// <summary>
        /// Returns exchange status for all currency codes
        /// </summary>
        public async Task<Dictionary<string, decimal>> GetWithdrawalFees()
        {
            ThrowIfDisposed();
            return await HttpWorker.QueryPublicAsync<Dictionary<string, decimal>>("/Public/GetWithdrawalFees");
        }

        /// <summary>
        /// Returns exchange status for all currency codes
        /// </summary>
        public async Task<IEnumerable<DepositFee>> GetDepositFees()
        {
            ThrowIfDisposed();
            return await HttpWorker.QueryPublicAsync<IEnumerable<DepositFee>>("/Public/GetDepositFees");
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

            return await HttpWorker.QueryPrivateAsync<BankOrder>("/Private/PlaceLimitOrder", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<BankOrder>("/Private/PlaceMarketOrder", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<BankOrder>("/Private/CancelOrder", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<Page<BankHistoryOrder>>("/Private/GetOpenOrders", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<Page<BankHistoryOrder>>("/Private/GetClosedOrders", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<Page<BankHistoryOrder>>("/Private/GetClosedFilledOrders", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<BankOrder>("/Private/GetOrderDetails", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<IEnumerable<Account>>("/Private/GetAccounts", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<IEnumerable<BrokerageFee>>("/Private/GetBrokerageFees", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<Page<Transaction>>("/Private/GetTransactions", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<BitcoinDepositAddress>("/Private/GetBitcoinDepositAddress", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<DigitalCurrencyDepositAddress>("/Private/GetDigitalCurrencyDepositAddress", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<Page<BitcoinDepositAddress>>("/Private/GetBitcoinDepositAddresses", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<Page<DigitalCurrencyDepositAddress>>("/Private/GetDigitalCurrencyDepositAddresses", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<BitcoinDepositAddress>("/Private/SynchBitcoinAddressWithBlockchain", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<DigitalCurrencyDepositAddress>("/Private/SynchDigitalCurrencyDepositAddressWithBlockchain", data).ConfigureAwait(false);
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

            await HttpWorker.QueryPrivateAsync("/Private/WithdrawBitcoin", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates digital currency withdrawal request
        /// </summary>
        /// <param name="withdrawalAmount">withdrawal amount</param>
        /// <param name="withdrawalAddress">digital address to withdraw</param>
        /// <param name="comment">withdrawal comment</param>
        /// <param name="primaryCurrency">optional primary currency</param>
        [Obsolete("Use overload that accepts DigitalWithdrawalRequest instead.")]
        public CryptoWithdrawal WithdrawDigitalCurrency(decimal withdrawalAmount, string withdrawalAddress, string comment, CurrencyCode primaryCurrency)
        {
            return WithdrawDigitalCurrencyAsync(withdrawalAmount, withdrawalAddress, comment, primaryCurrency).Result;
        }

        /// <summary>
        /// Creates digital currency withdrawal request
        /// </summary>
        /// <param name="withdrawalAmount">withdrawal amount</param>
        /// <param name="withdrawalAddress">digital address to withdraw</param>
        /// <param name="comment">withdrawal comment</param>
        /// <param name="primaryCurrency">primary currency</param>
        [Obsolete("Use overload that accepts DigitalWithdrawalRequest instead.")]
        public async Task<CryptoWithdrawal> WithdrawDigitalCurrencyAsync(decimal withdrawalAmount, string withdrawalAddress, string comment, CurrencyCode primaryCurrency)
        {
            var request = new DigitalWithdrawalRequest
            {
                Amount = withdrawalAmount
                ,Address = withdrawalAddress
                ,Comment = comment
                ,Currency = primaryCurrency
            };
            return await WithdrawDigitalCurrencyAsync(request);
        }

        public CryptoWithdrawal WithdrawDigitalCurrency(DigitalWithdrawalRequest withdrawRequest)
        {
            return WithdrawDigitalCurrencyAsync(withdrawRequest).Result;
        }

        public async Task<CryptoWithdrawal> WithdrawDigitalCurrencyAsync(DigitalWithdrawalRequest withdrawRequest)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();

            data.amount = withdrawRequest.Amount.ToString(CultureInfo.InvariantCulture);
            data.withdrawalAddress = withdrawRequest.Address;
            data.comment = withdrawRequest.Comment;
            data.primaryCurrencyCode = withdrawRequest.Currency.ToString();

            if (!string.IsNullOrEmpty(withdrawRequest.DestinationTag))
            {
                data.destinationTag = withdrawRequest.DestinationTag;
            }

            return await HttpWorker.QueryPrivateAsync<CryptoWithdrawal>("/Private/WithdrawDigitalCurrency", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<FiatWithdrawalRequest>("/Private/RequestFiatWithdrawal", data).ConfigureAwait(false);
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

            return await HttpWorker.QueryPrivateAsync<Page<TradeDetails>>("/Private/GetTrades", data).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves trades that related to the specified order
        /// </summary>
        /// <param name="orderGuid">order guid</param>
        /// <returns>a list of specified order's trades</returns>
        public async Task<IEnumerable<TradeDetails>> GetTrades2Async(Guid orderGuid)
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();
            data.orderGuid = orderGuid.ToString();

            return await HttpWorker.QueryPrivateAsync<IEnumerable<TradeDetails>>("/Private/GetTrades2", data).ConfigureAwait(false);
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

        public async Task<DepositLimits> GetDepositLimits()
        {
            ThrowIfDisposed();
            ThrowIfPublicClient();

            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();

            return await HttpWorker.QueryPrivateAsync<DepositLimits>("/Private/GetDepositLimits", data).ConfigureAwait(false);
        }

        public async Task<Dictionary<string, List<TransactionLimit>>> GetWithdrawalLimits()
        {
            dynamic data = new ExpandoObject();
            data.apiKey = _apiKey;
            data.nonce = GetNonce();

            return await HttpWorker.QueryPrivateAsync<Dictionary<string, List<TransactionLimit>>>("/Private/GetWithdrawalLimits", data).ConfigureAwait(false);
        }

        #endregion //Private API

        #region Helpers


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
            if (string.IsNullOrWhiteSpace(_apiKey) || string.IsNullOrWhiteSpace(HttpWorker.ApiSecret))
            {
                throw new InvalidOperationException("This instance of Client can access Public API only. Use CreatePrivate method to create instance of client to call Private API.");
            }
        }

        /// <summary>
        /// Helper method to get nonce.
        /// </summary>
        private string GetNonce() => GetNonceProvider();
        
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
                if (HttpWorker != null)
                {
                    HttpWorker.Dispose();
                    HttpWorker = null;
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