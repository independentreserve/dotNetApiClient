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
            get { return new MethodMetadata() {Name = "GetOrderBook", Description = "GetOrderBook", Parameters = new string[] {"primaryCurrency", "secondaryCurrency"}}; }
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

        //PlaceLimitOrder(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, OrderType orderType, decimal price, decimal volume)
        public static MethodMetadata PlaceLimitOrder
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "PlaceLimitOrder",
                           Description = "PlaceLimitOrder",
                           Parameters = new[] {"primaryCurrency", "secondaryCurrency", "limitOrderType", "limitOrderPrice", "orderVolume"}
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
                           Parameters = new[] {"primaryCurrency", "secondaryCurrency", "marketOrderType", "orderVolume"}
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
                           Parameters = new[] {"orderGuid"}
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

        //GetTransactions(Guid accountGuid, DateTime? fromTimestampUtc, DateTime? toTimestampUtc, int pageIndex, int pageSize)
        public static MethodMetadata GetTransactions
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "GetTransactions",
                           Description = "GetTransactions",
                           Parameters = new[] {"accountGuid", "fromTimestampUtc", "toTimestampUtc", "pageIndex", "pageSize"}
                       };
            }
        }

        //GetBitcoinDepositAddress()
        public static MethodMetadata GetBitcoinDepositAddress
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "GetBitcoinDepositAddress",
                           Description = "GetBitcoinDepositAddress",
                           Parameters = new string[] {}
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
                           Parameters = new[] {"secondaryCurrency", "withdrawalAmount", "withdrawalBankAccountName"}
                       };
            }
        }

        //SynchBitcoinAddressWithBlockchain()
        public static MethodMetadata SynchBitcoinAddressWithBlockchain
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "SynchBitcoinAddressWithBlockchain",
                           Description = "SynchBitcoinAddressWithBlockchain",
                           Parameters = new[] {"address"}
                       };
            }
        }

        //WithdrawBitcoin()
        public static MethodMetadata WithdrawBitcoin
        {
            get
            {
                return new MethodMetadata()
                       {
                           Name = "WithdrawBitcoin",
                           Description = "WithdrawBitcoin",
                           Parameters = new[] {"withdrawalAmount", "address"}
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
                    Parameters = new[] { "pageIndex", "pageSize" }
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