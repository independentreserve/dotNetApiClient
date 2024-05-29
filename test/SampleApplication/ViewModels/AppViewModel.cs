using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using IndependentReserve.DotNetClientApi;
using IndependentReserve.DotNetClientApi.Data;
using IndependentReserve.DotNetClientApi.Data.Shop;
using NLog;
using SampleApplication.Annotations;

namespace SampleApplication.ViewModels
{
    /// <summary>
    /// ViewModel for Main application window; contains collection of methods which can be called, and notifyable properties to store all specified method parameters
    /// </summary>
    public class AppViewModel : INotifyPropertyChanged
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();
        private MethodMetadata _selectedMethod;
        private string _lastRequestParameters;
        private string _lastRequestResponse;
        private string _lastRequestUrl;
        private string _lastRequestDuration;
        private CurrencyCode _primaryCurrency;
        private CurrencyCode _secondaryCurrency;
        private int? _numberOfRecentTradesToRetrieve;
        private int? _numberOfHoursInThePastToRetrieve;
        private int? _pageIndex;
        private int? _pageSize;
        private OrderType _limitOrderType;
        private OrderType _marketOrderType;
        private TradeAction _shopOrderType;
        private decimal? _limitOrderPrice;
        private decimal? _orderVolume;
        private CurrencyType _volumeCurrencyType;
        private string _orderGuid;
        private string _quoteGuid;
        private string _dealGuid;
        private DateTime? _fromTimestampUtc;
        private DateTime? _toTimestampUtc;
        private string _accountGuid;
        private decimal _withdrawalAmount;
        private string _withdrawalBankAccountName;
        private string _bankAccountGuid;
        private bool _useNpp;
        private string _address;
        private string _tag;
        private string _comment;
        private string _customId;
        private string _transactionGuid;
        private string _orderGuids;
        private decimal? _maxDepthVolumeOrderBook;
        private decimal? _maxDepthValueOrderBook;
        private string _bankOrderClientId;
        private decimal? _allowedSlippagePercent;
        private TimeInForce? _timeInForce;
        private bool _includeTotals;
        private string _withdrawalClientId;

        public AppViewModel(ApiConfig apiConfig)
        {
            _primaryCurrency = CurrencyCode.Xbt;
            _secondaryCurrency = CurrencyCode.Usd;
            _numberOfHoursInThePastToRetrieve = 96;
            _numberOfRecentTradesToRetrieve = 10;
            _pageIndex = 1;
            _pageSize = 10;
            _limitOrderType = OrderType.LimitBid;
            _marketOrderType = OrderType.MarketOffer;
            _shopOrderType = TradeAction.Buy;
            _limitOrderPrice = 500;
            _orderVolume = 0.1m;
            _volumeCurrencyType = CurrencyType.Primary;
            _orderGuid = string.Empty;
            _toTimestampUtc = null;
            _withdrawalAmount = 50;
            _withdrawalBankAccountName = null;
            _address = null;
            _transactionGuid = null;
            _orderGuids = string.Empty;
            _maxDepthVolumeOrderBook = null;
            _maxDepthValueOrderBook = null;
            _bankOrderClientId = null;
            _allowedSlippagePercent = null;
            _timeInForce = null;
            _quoteGuid = Guid.NewGuid().ToString();
            _dealGuid = Guid.NewGuid().ToString();
            _includeTotals = true;

            ApiConfig = apiConfig;

            SetTransactionTypes(apiConfig);

            SetFromTimestampUtc();

            PropertyChanged += OnPropertyChanged;
        }

        private void SetTransactionTypes(ApiConfig apiConfig)
        {
            TransactionTypes = new ObservableCollection<TransactionTypeViewModel>();
            Log.Info($"Connecting to '{apiConfig.BaseUrl}'");
            using (var client = Client.Create(apiConfig))
            {
                IEnumerable<TransactionType> types = null;
                try
                {
                    types = client.GetValidTransactionTypes();
                }
                catch (Exception e)
                {
                    // If this fails it could be a certificate error in dev/test env
                    var deepest = e.GetBaseException();
                    Log.Error(deepest, $"GetValidTransactionTypes failed. LastRequestUrl='{client.LastRequestUrl}'");
                    throw;
                }
                foreach (var transactionType in types)
                {
                    TransactionTypes.Add(new TransactionTypeViewModel { IsSelected = false, Type = transactionType });
                }
            }
        }

        private void SetFromTimestampUtc()
        {
            FromTimestampUtc = DateTime.Now.AddDays(-90);
        }

        private void ResetFromTimestampUtc()
        {
            FromTimestampUtc = null;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedMethod))
            {
                if (SelectedMethod == MethodMetadata.GetClosedFilledOrders)
                {
                    ResetFromTimestampUtc();
                }
                else
                {
                    SetFromTimestampUtc();
                }
            }
        }

        public MethodMetadata[] Methods
        {
            get
            {
                var methods = new[]
                    {
                        MethodMetadata.Null,
                        MethodMetadata.GetValidPrimaryCurrencyCodes,
                        MethodMetadata.GetValidSecondaryCurrencyCodes,
                        MethodMetadata.GetValidLimitOrderTypes,
                        MethodMetadata.GetValidMarketOrderTypes,
                        MethodMetadata.GetValidOrderTypes,
                        MethodMetadata.GetValidTransactionTypes,
                        MethodMetadata.GetMarketSummary,
                        MethodMetadata.GetOrderBook,
                        MethodMetadata.GetAllOrders,
                        MethodMetadata.GetTradeHistorySummary,
                        MethodMetadata.GetRecentTrades,
                        MethodMetadata.GetFxRates,
                        MethodMetadata.PlaceLimitOrder,
                        MethodMetadata.PlaceMarketOrder,
                        MethodMetadata.CancelOrder,
                        MethodMetadata.CancelOrders,
                        MethodMetadata.GetAccounts,
                        MethodMetadata.GetOpenOrders,
                        MethodMetadata.GetClosedOrders,
                        MethodMetadata.GetClosedFilledOrders,
                        MethodMetadata.GetOrderDetails,
                        MethodMetadata.GetTransactions,
                        MethodMetadata.GetCryptoDeposits,
                        MethodMetadata.GetDigitalCurrencyDepositAddress,
                        MethodMetadata.GetDigitalCurrencyDepositAddresses,
                        MethodMetadata.NewDepositAddress,
                        MethodMetadata.SynchDigitalCurrencyDepositAddressWithBlockchain,
                        MethodMetadata.WithdrawDigitalCurrency,
                        MethodMetadata.GetDigitalCurrencyWithdrawal,
                        MethodMetadata.GetFiatBankAccounts,
                        MethodMetadata.RequestFiatWithdrawal,
                        MethodMetadata.WithdrawFiatCurrency,
                        MethodMetadata.GetFiatWithdrawal,
                        MethodMetadata.GetTrades,
                        MethodMetadata.GetTradesByOrder,
                        MethodMetadata.GetBrokerageFees,
                        MethodMetadata.GetEvents,
                        MethodMetadata.GetExchangeStatus,
                        MethodMetadata.GetFiatWithdrawalFees,
                        MethodMetadata.GetDepositFees,
                        MethodMetadata.GetWithdrawalLimits,
                        MethodMetadata.GetDepositLimits,
                        MethodMetadata.GetOrderMinimumVolumes,
                        MethodMetadata.GetCryptoWithdrawalFees,
                        MethodMetadata.GetPrimaryCurrencyConfig,

                    }
                    .OrderBy(x => x.Name)
                    .ToArray();

                return methods;
            }
        }

        /// <summary>
        /// Gets or sets currently selected method which will be called if user press "Call It" button
        /// </summary>
        public MethodMetadata SelectedMethod
        {
            get { return _selectedMethod; }
            set
            {
                if (_selectedMethod != value)
                {
                    _selectedMethod = value;
                    OnPropertyChanged();
                }
            }
        }

        public ApiConfig ApiConfig { get; private set; }

        public ObservableCollection<TransactionTypeViewModel> TransactionTypes { get; set; }

        #region client profiler information
        /// <summary>
        /// Stores last request parameters obtained from client
        /// </summary>
        public string LastRequestParameters
        {
            get { return _lastRequestParameters; }
            set
            {
                if (_lastRequestParameters != value)
                {
                    _lastRequestParameters = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Stores last request response obtained from client
        /// </summary>
        public string LastRequestResponse
        {
            get { return _lastRequestResponse; }
            set
            {
                if (_lastRequestResponse != value)
                {
                    _lastRequestResponse = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Stores last request url (and HTTP method) obtained from client
        /// </summary>
        public string LastRequestUrl
        {
            get { return _lastRequestUrl; }
            set
            {
                if (_lastRequestUrl != value)
                {
                    _lastRequestUrl = value;
                    OnPropertyChanged();
                }

            }
        }

        public string LastRequestDuration
        {
            get => _lastRequestDuration;
            set
            {
                if (_lastRequestDuration != value)
                {
                    _lastRequestDuration = value;
                    OnPropertyChanged();
                }

            }
        }

        #endregion

        #region method parameters
        /// <summary>
        /// Method parameter - primary currency
        /// </summary>
        public CurrencyCode PrimaryCurrency
        {
            get { return _primaryCurrency; }
            set
            {
                if (_primaryCurrency != value)
                {
                    _primaryCurrency = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Method parameter - secondary currency
        /// </summary>
        public CurrencyCode SecondaryCurrency
        {
            get { return _secondaryCurrency; }
            set
            {
                if (_secondaryCurrency != value)
                {
                    _secondaryCurrency = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Method parameter - number of hours in the past to retrieve - used by public API method GetTradeHistorySummary
        /// </summary>
        public int? NumberOfHoursInThePastToRetrieve
        {
            get { return _numberOfHoursInThePastToRetrieve; }
            set
            {

                if (_numberOfHoursInThePastToRetrieve != value)
                {
                    _numberOfHoursInThePastToRetrieve = value;
                    OnPropertyChanged();
                }

            }
        }

        /// <summary>
        /// Method parameter - number of recent trades to retrieve - used by public API method GetRecentTrades
        /// </summary>
        public int? NumberOfRecentTradesToRetrieve
        {
            get { return _numberOfRecentTradesToRetrieve; }
            set
            {
                if (_numberOfRecentTradesToRetrieve != value)
                {
                    _numberOfRecentTradesToRetrieve = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Method parameter - page index - used by several private API methods which returns data in pages
        /// </summary>
        public int? PageIndex
        {
            get { return _pageIndex; }
            set
            {
                if (value == _pageIndex) return;
                _pageIndex = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - page size - used by several private API methods which returns data in pages
        /// </summary>
        public int? PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value == _pageSize) return;
                _pageSize = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - limit order type - used by private API method PlaceLimitOrder
        /// </summary>
        public OrderType LimitOrderType
        {
            get { return _limitOrderType; }
            set
            {
                if (value == _limitOrderType) return;
                _limitOrderType = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - market order type - used by private API method PlaceMarketOrder
        /// </summary>
        public OrderType MarketOrderType
        {
            get { return _marketOrderType; }
            set
            {
                if (value == _marketOrderType) return;
                _marketOrderType = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - shop order type - used by private API method RequestQuote
        /// </summary>
        public TradeAction ShopOrderType
        {
            get { return _shopOrderType; }
            set
            {
                if (value == _shopOrderType) return;
                _shopOrderType = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - limit order price - used by private API method PlaceLimitOrder
        /// </summary>
        public decimal? LimitOrderPrice
        {
            get { return _limitOrderPrice; }
            set
            {
                if (value == _limitOrderPrice) return;
                _limitOrderPrice = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - order volume - used by private API methods PlaceLimitOrder and PlaceMarketOrder
        /// </summary>
        public decimal? OrderVolume
        {
            get { return _orderVolume; }
            set
            {
                if (value == _orderVolume) return;
                _orderVolume = value;
                OnPropertyChanged();
            }
        }

        /// Method parameter - order volume - used by private API method PlaceMarketOrder
        /// </summary>
        public CurrencyType VolumeCurrencyType
        {
            get { return _volumeCurrencyType; }
            set
            {
                if (value == _volumeCurrencyType) return;
                _volumeCurrencyType = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - order guid - used by private API method CancelOrder
        /// </summary>
        public string OrderGuid
        {
            get { return _orderGuid; }
            set
            {
                if (value == _orderGuid) return;
                _orderGuid = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - order guids - used by private API method CancelOrders
        /// </summary>
        public string OrderGuids
        {
            get { return _orderGuids; }
            set
            {
                if (value == _orderGuids) return;
                _orderGuids = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - quote guid - used by private API method ExecuteQuote
        /// </summary>
        public string QuoteGuid
        {
            get { return _quoteGuid; }
            set
            {
                if (value == _quoteGuid) return;
                _quoteGuid = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - deal guid - used by private API method GetDealDetails
        /// </summary>
        public string DealGuid
        {
            get { return _dealGuid; }
            set
            {
                if (value == _dealGuid) return;
                _dealGuid = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - FromTimestampUtc - used by private API methods: GetTransactions, GetTrades
        /// </summary>
        public DateTime? FromTimestampUtc
        {
            get { return _fromTimestampUtc; }
            set
            {
                if (value.Equals(_fromTimestampUtc)) return;
                _fromTimestampUtc = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - ToTimestampUtc - used by private API methods: GetTransactions, GetTrades
        /// </summary>
        public DateTime? ToTimestampUtc
        {
            get { return _toTimestampUtc; }
            set
            {
                if (value.Equals(_toTimestampUtc)) return;
                _toTimestampUtc = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - AccountGuid - used by private API method GetTransactions
        /// </summary>
        public string AccountGuid
        {
            get { return _accountGuid; }
            set
            {
                if (value == _accountGuid) return;
                _accountGuid = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - WithdrawalAmount- used by private API method RequestFiatWithdrawal
        /// </summary>
        public decimal WithdrawalAmount
        {
            get { return _withdrawalAmount; }
            set
            {
                if (value == _withdrawalAmount) return;
                _withdrawalAmount = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - WithdrawalBankAccountName - used by private API method RequestFiatWithdrawal
        /// </summary>
        public string WithdrawalBankAccountName
        {
            get { return _withdrawalBankAccountName; }
            set
            {
                if (value == _withdrawalBankAccountName) return;
                _withdrawalBankAccountName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - BankAccountGuid - used by private API method WithdrawFiatCurrency
        /// </summary>
        public string BankAccountGuid
        {
            get { return _bankAccountGuid; }
            set
            {
                if (value == _bankAccountGuid) return;
                _bankAccountGuid = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - UseNpp - used by private API method WithdrawFiatCurrency
        /// </summary>
        public bool UseNpp
        {
            get { return _useNpp; }
            set
            {
                if (value == _useNpp) return;
                _useNpp = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - Address - used by private API method SynchBitcoinAddressWithBlockchain
        /// </summary>
        public string Address
        {
            get { return _address; }
            set
            {
                if (value == _address) return;
                _address = value;
                OnPropertyChanged();
            }
        }

        public string Tag
        {
            get { return _tag; }
            set
            {
                if (value == _tag) return;
                _tag = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - Comment - used by private API withdrawal methods
        /// </summary>
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (value == _comment) return;
                _comment = value;
                OnPropertyChanged();
            }
        }

        public string CustomId
        {
            get { return _customId; }
            set
            {
                if (value == _customId) return;
                _customId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Method parameter - TransactionGuid - used by private API method GetDigitalCurrencyWithdrawal
        /// </summary>
        public string TransactionGuid
        {
            get => _transactionGuid;
            set
            {
                if (value == _transactionGuid) return;
                _transactionGuid = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Limit the number of orders returned to the client on both sides of the order book based on the cumulative volume of orders
        /// </summary>
        public decimal? MaxDepthVolumeOrderBook
        {
            get { return _maxDepthVolumeOrderBook; }
            set
            {

                if (_maxDepthVolumeOrderBook != value)
                {
                    _maxDepthVolumeOrderBook = value;
                    OnPropertyChanged();
                }

            }
        }

        /// <summary>
        /// Limit the number of orders returned to the client on both sides of the order book based on the cumulative value (vol*price)
        /// </summary>
        public decimal? MaxDepthValueOrderBook
        {
            get { return _maxDepthValueOrderBook; }
            set
            {

                if (_maxDepthValueOrderBook != value)
                {
                    _maxDepthValueOrderBook = value;
                    OnPropertyChanged();
                }

            }
        }

        /// <summary>
        /// Client order Id
        /// </summary>
        public string BankOrderClientId
        {
            get { return _bankOrderClientId; }
            set
            {

                if (_bankOrderClientId != value)
                {
                    _bankOrderClientId = value;
                    OnPropertyChanged();
                }

            }
        }


        /// <summary>
        /// Slippage protection, in which the order will be rejected after the market has moved by the specified%.
        /// </summary>
        public decimal? AllowedSlippagePercent
        {
            get { return _allowedSlippagePercent; }
            set
            {

                if (_allowedSlippagePercent != value)
                {
                    _allowedSlippagePercent = value;
                    OnPropertyChanged();
                }

            }
        }

        public TimeInForce? TimeInForce
        {
            get { return _timeInForce; }
            set
            {
                if (_timeInForce != value)
                {
                    _timeInForce = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Method parameter - allows you to disable the calculation of TotalItems used in pagination (this allows you to execute the request faster)
        /// </summary>
        public bool IncludeTotals
        {
            get { return _includeTotals; }
            set
            {
                if (value == _includeTotals) return;
                _includeTotals = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Client withdrawal Id
        /// </summary>
        public string WithdrawalClientId
        {
            get => _withdrawalClientId;
            set
            {

                if (_withdrawalClientId != value)
                {
                    _withdrawalClientId = value;
                    OnPropertyChanged();
                }

            }
        }

        #endregion //method parameters

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion //INotifyPropertyChanged implementation
    }
}
