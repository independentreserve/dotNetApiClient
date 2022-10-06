using System;

namespace SampleApplication.ViewModels
{
    /// <summary>
    /// This struct used to describe method and its parameters; used to construct UI of the demo app
    /// </summary>
    public struct MethodMetadata
    {
        /// <summary>
        /// Name of the method
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Description of the method
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Name of parameters this method should be called with
        /// </summary>
        public string[] Parameters { get; private set; }


        /// <summary>
        /// Fake 'Null' method - used for "empty" choice in Methods combobox
        /// </summary>
        public static MethodMetadata Null
        {
            get { return new MethodMetadata() {Name = String.Empty, Description = "Select method to call ...", Parameters = new string[] {}}; }
        }

        //GetValidPrimaryCurrencyCodes()
        public static MethodMetadata GetValidPrimaryCurrencyCodes
        {
            get { return new MethodMetadata() {Name = "GetValidPrimaryCurrencyCodes", Description = "GetValidPrimaryCurrencyCodes", Parameters = new string[] {}}; }
        }

        //GetValidSecondaryCurrencyCodes()
        public static MethodMetadata GetValidSecondaryCurrencyCodes
        {
            get { return new MethodMetadata() {Name = "GetValidSecondaryCurrencyCodes", Description = "GetValidSecondaryCurrencyCodes", Parameters = new string[] {}}; }
        }

        //GetValidLimitOrderTypes()
        public static MethodMetadata GetValidLimitOrderTypes
        {
            get { return new MethodMetadata() {Name = "GetValidLimitOrderTypes", Description = "GetValidLimitOrderTypes", Parameters = new string[] {}}; }
        }

        //GetValidMarketOrderTypes()
        public static MethodMetadata GetValidMarketOrderTypes
        {
            get { return new MethodMetadata() {Name = "GetValidMarketOrderTypes", Description = "GetValidMarketOrderTypes", Parameters = new string[] {}}; }
        }

        //GetValidOrderTypes()
        public static MethodMetadata GetValidOrderTypes
        {
            get { return new MethodMetadata() { Name = "GetValidOrderTypes", Description = "GetValidOrderTypes", Parameters = new string[] { }}; }
        }

        //GetValidTransactionTypes()
        public static MethodMetadata GetValidTransactionTypes
        {
            get { return new MethodMetadata() { Name = "GetValidTransactionTypes", Description = "GetValidTransactionTypes", Parameters = new string[] { } }; }
        }

        //GetMarketSummary(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency)
        public static MethodMetadata GetMarketSummary
        {
            get { return new MethodMetadata() {Name = "GetMarketSummary", Description = "GetMarketSummary", Parameters = new string[] {"primaryCurrency", "secondaryCurrency"}}; }
        }

        //GetOrderBook(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency)
        public static MethodMetadata GetOrderBook
        {
            get { return new MethodMetadata() {Name = "GetOrderBook", Description = "GetOrderBook", Parameters = new string[] {"primaryCurrency", "secondaryCurrency", "maxDepthVolumeOrderBook", "maxDepthValueOrderBook" } }; }
        }

        //GetAllOrders(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency)
        public static MethodMetadata GetAllOrders
        {
            get { return new MethodMetadata() {Name = "GetAllOrders", Description = "GetAllOrders", Parameters = new string[] {"primaryCurrency", "secondaryCurrency", "maxDepthVolumeOrderBook", "maxDepthValueOrderBook" } }; }
        }

        //GetTradeHistorySummary(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int numberOfHoursInThePastToRetrieve
        public static MethodMetadata GetTradeHistorySummary
        {
            get { return new MethodMetadata() {Name = "GetTradeHistorySummary", Description = "GetTradeHistorySummary", Parameters = new string[] {"primaryCurrency", "secondaryCurrency", "numberOfHoursInThePastToRetrieve"}}; }
        }

        //GetRecentTrades(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int numberOfRecentTradesToRetrieve)
        public static MethodMetadata GetRecentTrades
        {
            get { return new MethodMetadata() {Name = "GetRecentTrades", Description = "GetRecentTrades", Parameters = new string[] {"primaryCurrency", "secondaryCurrency", "numberOfRecentTradesToRetrieve"}}; }
        }

        //GetFxRates()
        public static MethodMetadata GetFxRates
        {
            get { return new MethodMetadata() { Name = "GetFxRates", Description = "GetFxRates", Parameters = new string[] { } }; }
        }

        //PlaceLimitOrder(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, OrderType orderType, decimal price, decimal volume)
        public static MethodMetadata PlaceLimitOrder
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "PlaceLimitOrder",
                           Description = "PlaceLimitOrder",
                           Parameters = new[] {"primaryCurrency", "secondaryCurrency", "limitOrderType", "limitOrderPrice", "orderVolume", "bankOrderClientId" }
                       };
            }
        }

        //PlaceMarketOrder(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, OrderType orderType, decimal volume)
        public static MethodMetadata PlaceMarketOrder
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "PlaceMarketOrder",
                           Description = "PlaceMarketOrder",
                           Parameters = new[] {"primaryCurrency", "secondaryCurrency", "marketOrderType", "orderVolume", "volumeCurrencyType", "bankOrderClientId" }
                       };
            }
        }

        //CancelOrder(Guid orderGuid)
        public static MethodMetadata CancelOrder
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "CancelOrder",
                           Description = "CancelOrder",
                           Parameters = new[] {"orderGuid"}
                       };
            }
        }

        //GetOpenOrders(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int pageIndex, int pageSize)
        public static MethodMetadata GetOpenOrders
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "GetOpenOrders",
                           Description = "GetOpenOrders",
                           Parameters = new[] {"primaryCurrency", "secondaryCurrency", "pageIndex", "pageSize"}
                       };
            }
        }

        //GetClosedOrders(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int pageIndex, int pageSize)
        public static MethodMetadata GetClosedOrders
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "GetClosedOrders",
                           Description = "GetClosedOrders",
                           Parameters = new[] {"primaryCurrency", "secondaryCurrency", "pageIndex", "pageSize"}
                       };
            }
        }

        //GetClosedFilledOrders(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int pageIndex, int pageSize)
        public static MethodMetadata GetClosedFilledOrders
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "GetClosedFilledOrders",
                           Description = "GetClosedFilledOrders",
                           Parameters = new[] {"primaryCurrency", "secondaryCurrency", "pageIndex", "pageSize"}
                       };
            }
        }

        //GetOrderDetails(Guid orderGuid)
        public static MethodMetadata GetOrderDetails
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "GetOrderDetails",
                           Description = "GetOrderDetails",
                           Parameters = new[] {"orderGuid", "bankOrderClientId" }
                       };
            }
        }

        //GetAccounts()
        public static MethodMetadata GetAccounts
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "GetAccounts",
                           Description = "GetAccounts",
                           Parameters = new string[] {}
                       };
            }
        }

        // GetBrokerageFees()
        public static MethodMetadata GetBrokerageFees
        {
            get
            {
                return new MethodMetadata
                {
                    Name = "GetBrokerageFees",
                    Description = "GetBrokerageFees",
                    Parameters = new string[] {}
                };
            }
        }

        //GetTransactions(Guid accountGuid, DateTime? fromTimestampUtc, DateTime? toTimestampUtc, int pageIndex, int pageSize)
        public static MethodMetadata GetTransactions
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "GetTransactions",
                           Description = "GetTransactions",
                           Parameters = new[] {"accountGuid", "fromTimestampUtc", "toTimestampUtc", "pageIndex", "pageSize", "txTypes"}
                       };
            }
        }

        public static MethodMetadata GetCryptoDeposits
        {
            get
            {
                return new MethodMetadata()
                {
                    Name = "GetCryptoDeposits",
                    Description = "GetCryptoDeposits",
                    Parameters = new[] { "primaryCurrency", "fromTimestampUtc", "toTimestampUtc", "pageIndex", "pageSize" }
                };
            }
        }

        //GetDigitalCurrencyDepositAddress()
        public static MethodMetadata GetDigitalCurrencyDepositAddress
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "GetDigitalCurrencyDepositAddress",
                           Description = "GetDigitalCurrencyDepositAddress",
                           Parameters = new[] {"primaryCurrency"}
                       };
            }
        }

        //NewDepositAddress()
        public static MethodMetadata NewDepositAddress
        {
            get
            {
                return new MethodMetadata()
                {
                    Name = "NewDepositAddress",
                    Description = "NewDepositAddress",
                    Parameters = new[] { "primaryCurrency" }
                };
            }
        }

        //GetDigitalCurrencyDepositAddresses()
        public static MethodMetadata GetDigitalCurrencyDepositAddresses
        {
            get
            {
                return new MethodMetadata()
                {
                    Name = "GetDigitalCurrencyDepositAddresses",
                    Description = "GetDigitalCurrencyDepositAddresses",
                    Parameters = new [] { "primaryCurrency", "pageIndex", "pageSize"}
                };
            }
        }

        //RequestFiatWithdrawal()
        public static MethodMetadata RequestFiatWithdrawal
        {
            get
            {
                return new MethodMetadata()
                {
                    Name = "RequestFiatWithdrawal",
                    Description = "RequestFiatWithdrawal",
                    Parameters = new[] { "secondaryCurrency", "withdrawalAmount", "withdrawalBankAccountName", "comment" }
                };
            }
        }

        //GetFiatBankAccounts()
        public static MethodMetadata GetFiatBankAccounts
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "GetFiatBankAccounts",
                           Description = "GetFiatBankAccounts",
                           Parameters = Array.Empty<string>()
                       };
            }
        }

        public static MethodMetadata WithdrawFiatCurrency
        {
            get
            {
                return new MethodMetadata()
                {
                    Name = "WithdrawFiatCurrency",
                    Description = "WithdrawFiatCurrency",
                    Parameters = new[] { "secondaryCurrency", "withdrawalAmount", "bankAccountGuid", "useNpp", "comment" }
                };
            }
        }

        //GetFiatWithdrawal()
        public static MethodMetadata GetFiatWithdrawal
        {
            get
            {
                return new MethodMetadata()
                {
                    Name = "GetFiatWithdrawal",
                    Description = "GetFiatWithdrawal",
                    Parameters = new[] { "transactionGuid" }
                };
            }
        }

        //SynchDigitalCurrencyDepositAddressWithBlockchain()
        public static MethodMetadata SynchDigitalCurrencyDepositAddressWithBlockchain
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "SynchDigitalCurrencyDepositAddressWithBlockchain",
                           Description = "SynchDigitalCurrencyDepositAddressWithBlockchain",
                           Parameters = new[] {"address", "primaryCurrency"}
                       };
            }
        }

        //WithdrawDigitalCurrency()
        public static MethodMetadata WithdrawDigitalCurrency
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "WithdrawDigitalCurrnecy",
                           Description = "WithdrawDigitalCurrency",
                           Parameters = new[] {"withdrawalAmount", "address", "tag", "comment", "primaryCurrency" }
                       };
            }
        }

        //GetDigitalCurrencyWithdrawal()
        public static MethodMetadata GetDigitalCurrencyWithdrawal
        {
            get
            {
                return new MethodMetadata()
                {
                    Name = "GetDigitalCurrencyWithdrawal",
                    Description = "GetDigitalCurrencyWithdrawal",
                    Parameters = new[] { "transactionGuid" }
                };
            }
        }

        //GetTrades(int pageIndex, int pageSize)
        public static MethodMetadata GetTrades
        {
            get
            {
                return new MethodMetadata()
                {
                    Name = "GetTrades",
                    Description = "GetTrades",
                    Parameters = new[] { "pageIndex", "pageSize", "fromTimestampUtc", "toTimestampUtc" }
                };
            }
        }

        //GetTradesByOrder(string orderGuid)
        public static MethodMetadata GetTradesByOrder
        {
            get
            {
                return new MethodMetadata()
                {
                    Name = "GetTradesByOrder",
                    Description = "GetTradesByOrder",
                    Parameters = new[] { "orderGuid", "pageIndex", "pageSize" }
                };
            }
        }

        public static MethodMetadata GetEvents => new MethodMetadata()
        {
            Name = "GetEvents",
            Description = "GetEvents",
            Parameters = new string[] { }
        };

        public static MethodMetadata GetExchangeStatus =>
            new MethodMetadata()
            {
                Name = "GetExchangeStatus",
                Description = "GetExchangeStatus",
                Parameters = new string[] { }
            };

        public static MethodMetadata GetFiatWithdrawalFees =>
            new MethodMetadata()
            {
                Name = "GetFiatWithdrawalFees",
                Description = "GetFiatWithdrawalFees",
                Parameters = new string[] { }
            };


        public static MethodMetadata GetDepositFees =>
            new MethodMetadata()
            {
                Name = "GetDepositFees",
                Description = "GetDepositFees",
                Parameters = new string[] { }
            };

        public static MethodMetadata GetDepositLimits =>
            new MethodMetadata()
            {
                Name = "GetDepositLimits",
                Description = "GetDepositLimits",
                Parameters = new string[] { }
            };


        public static MethodMetadata GetWithdrawalLimits =>
            new MethodMetadata()
            {
                Name = "GetWithdrawalLimits",
                Description = "GetWithdrawalLimits",
                Parameters = new string[] { }
            };

        public static MethodMetadata GetOrderMinimumVolumes =>
            new MethodMetadata()
            {
                Name = "GetOrderMinimumVolumes",
                Description = "GetOrderMinimumVolumes",
                Parameters = new string[] { }
            };

        public static MethodMetadata GetCryptoWithdrawalFees =>
            new MethodMetadata()
            {
                Name = "GetCryptoWithdrawalFees",
                Description = "GetCryptoWithdrawalFees",
                Parameters = new string[] { }
            };

        public static MethodMetadata CancelOrders
        {
            get
            {
                return new MethodMetadata()
                {
                    Name = "CancelOrders",
                    Description = "CancelOrders",
                    Parameters = new[] { "orderGuids" }
                };
            }
        }

        public override bool Equals(Object obj)
        {
            return obj is MethodMetadata && this == (MethodMetadata) obj;
        }

        public override int GetHashCode()
        {
            return (Name ?? String.Empty).GetHashCode();
        }

        public static bool operator ==(MethodMetadata x, MethodMetadata y)
        {
            return x.Name == y.Name;
        }

        public static bool operator !=(MethodMetadata x, MethodMetadata y)
        {
            return !(x == y);
        }
    }
}