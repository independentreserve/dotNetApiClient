using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IndependentReserve.DotNetClientApi.Data;
using SampleApplication.Annotations;

namespace SampleApplication.ViewModels
{
    /// <summary>
    /// ViewModel for Main application window; contains collection of methods which can be called, and notifyable properties to store all specified method parameters
    /// </summary>
    public class AppViewModel:INotifyPropertyChanged
    {
        private readonly MethodMetadata[] _methods = new[]
        {
            MethodMetadata.Null,
            MethodMetadata.GetValidPrimaryCurrencyCodes, 
            MethodMetadata.GetValidSecondaryCurrencyCodes, 
            MethodMetadata.GetValidLimitOrderTypes, 
            MethodMetadata.GetValidMarketOrderTypes, 
            MethodMetadata.GetMarketSummary, 
            MethodMetadata.GetOrderBook, 
            MethodMetadata.GetTradeHistorySummary, 
            MethodMetadata.GetRecentTrades, 
            MethodMetadata.PlaceLimitOrder, 
            MethodMetadata.PlaceMarketOrder, 
            MethodMetadata.CancelOrder, 
            MethodMetadata.GetAccounts, 
            MethodMetadata.GetOpenOrders, 
            MethodMetadata.GetClosedOrders, 
            MethodMetadata.GetClosedFilledOrders, 
            MethodMetadata.GetOrderDetails,
            MethodMetadata.GetTransactions, 
            MethodMetadata.GetBitcoinDepositAddress, 
            MethodMetadata.SynchBitcoinAddressWithBlockchain,
            MethodMetadata.WithdrawBitcoin,
            MethodMetadata.RequestFiatWithdrawal,
            MethodMetadata.GetTrades
        };

        private MethodMetadata _selectedMethod;
        private string _lastRequestParameters;
        private string _lastRequestResponse;
        private string _lastRequestUrl;
        private CurrencyCode _primaryCurrency;
        private CurrencyCode _secondaryCurrency;
        private int? _numberOfRecentTradesToRetrieve;
        private int? _numberOfHoursInThePastToRetrieve;
        private int? _pageIndex;
        private int? _pageSize;
        private OrderType _limitOrderType;
        private OrderType _marketOrderType;
        private decimal? _limitOrderPrice;
        private decimal? _orderVolume;
        private string _orderGuid;
        private DateTime? _fromTimestampUtc;
        private DateTime? _toTimestampUtc;
        private string _accountGuid;
        private decimal _withdrawalAmount;
        private string _withdrawalBankAccountName;
        private string _address;

        public AppViewModel()
        {
            _primaryCurrency = CurrencyCode.Xbt;
            _secondaryCurrency = CurrencyCode.Usd;
            _numberOfHoursInThePastToRetrieve = 96;
            _numberOfRecentTradesToRetrieve = 10;
            _pageIndex = 1;
            _pageSize = 10;
            _limitOrderType = OrderType.LimitBid;
            _marketOrderType = OrderType.MarketOffer;
            _limitOrderPrice = 500;
            _orderVolume = 0.1m;
            _orderGuid = string.Empty;
            _fromTimestampUtc = new DateTime(2014,8,1);
            _toTimestampUtc = null;
            _withdrawalAmount = 50;
            _withdrawalBankAccountName = null;
            _address = null;
        }

        public MethodMetadata[] Methods
        {
            get { return _methods; }
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

        public string ApiKey { get; set; }
        public string ApiUrl { get; set; }

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
        /// Method parameter - FromTimestampUtc - used by private API method GetTransactions
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
        /// Method parameter - ToTimestampUtc - used by private API method GetTransactions
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
