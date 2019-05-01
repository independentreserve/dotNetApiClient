using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IndependentReserve.DotNetClientApi.Data;

namespace IndependentReserve.DotNetClientApi
{
    public interface IClient
    {
        string LastRequestHttpMethod { get; }
        string LastRequestParameters { get; }
        string LastRequestUrl { get; }
        string LastResponseRaw { get; }

        BankOrder CancelOrder(Guid orderGuid);
        Task<BankOrder> CancelOrderAsync(Guid orderGuid);

        IEnumerable<Account> GetAccounts();
        Task<IEnumerable<Account>> GetAccountsAsync();
        BitcoinDepositAddress GetBitcoinDepositAddress();
        Task<BitcoinDepositAddress> GetBitcoinDepositAddressAsync();
        Page<BitcoinDepositAddress> GetBitcoinDepositAddresses(int pageIndex, int pageSize);
        Task<Page<BitcoinDepositAddress>> GetBitcoinDepositAddressesAsync(int pageIndex, int pageSize);
        IEnumerable<BrokerageFee> GetBrokerageFees();
        Task<IEnumerable<BrokerageFee>> GetBrokerageFeesAsync();
        Page<BankHistoryOrder> GetClosedFilledOrders(CurrencyCode? primaryCurrency, CurrencyCode? secondaryCurrency, int pageIndex, int pageSize);
        Task<Page<BankHistoryOrder>> GetClosedFilledOrdersAsync(CurrencyCode? primaryCurrency, CurrencyCode? secondaryCurrency, int pageIndex, int pageSize);
        Page<BankHistoryOrder> GetClosedOrders(CurrencyCode? primaryCurrency, CurrencyCode? secondaryCurrency, int pageIndex, int pageSize);
        Task<Page<BankHistoryOrder>> GetClosedOrdersAsync(CurrencyCode? primaryCurrency, CurrencyCode? secondaryCurrency, int pageIndex, int pageSize);
        DigitalCurrencyDepositAddress GetDigitalCurrencyDepositAddress(CurrencyCode primaryCurrency);
        Task<DigitalCurrencyDepositAddress> GetDigitalCurrencyDepositAddressAsync(CurrencyCode primaryCurrency);
        Page<DigitalCurrencyDepositAddress> GetDigitalCurrencyDepositAddresses(CurrencyCode primaryCurrency, int pageIndex, int pageSize);
        Task<Page<DigitalCurrencyDepositAddress>> GetDigitalCurrencyDepositAddressesAsync(CurrencyCode primaryCurrency, int pageIndex, int pageSize);
        IEnumerable<FxRate> GetFxRates();
        Task<IEnumerable<FxRate>> GetFxRatesAsync();
        MarketSummary GetMarketSummary(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency);
        Task<MarketSummary> GetMarketSummaryAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency);
        Page<BankHistoryOrder> GetOpenOrders(CurrencyCode? primaryCurrency, CurrencyCode? secondaryCurrency, int pageIndex, int pageSize);
        Task<Page<BankHistoryOrder>> GetOpenOrdersAsync(CurrencyCode? primaryCurrency, CurrencyCode? secondaryCurrency, int pageIndex, int pageSize);
        OrderBook GetOrderBook(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency);
        Task<OrderBook> GetOrderBookAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency);
        OrderBookDetailed GetAllOrders(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency);
        Task<OrderBookDetailed> GetAllOrdersAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency);
        BankOrder GetOrderDetails(Guid orderGuid);
        Task<BankOrder> GetOrderDetailsAsync(Guid orderGuid);
        RecentTrades GetRecentTrades(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int numberOfRecentTradesToRetrieve);
        Task<RecentTrades> GetRecentTradesAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int numberOfRecentTradesToRetrieve);
        TradeHistorySummary GetTradeHistorySummary(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int numberOfHoursInThePastToRetrieve);
        Task<TradeHistorySummary> GetTradeHistorySummaryAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, int numberOfHoursInThePastToRetrieve);
        Page<TradeDetails> GetTrades(int pageIndex, int pageSize);
        Task<Page<TradeDetails>> GetTradesAsync(int pageIndex, int pageSize);
        Page<Transaction> GetTransactions(Guid accountGuid, DateTime? fromTimestampUtc, DateTime? toTimestampUtc, string[] txTypes, int pageIndex, int pageSize);
        Task<Page<Transaction>> GetTransactionsAsync(Guid accountGuid, DateTime? fromTimestampUtc, DateTime? toTimestampUtc, string[] txTypes, int pageIndex, int pageSize);
        IEnumerable<OrderType> GetValidLimitOrderTypes();
        Task<IEnumerable<OrderType>> GetValidLimitOrderTypesAsync();
        IEnumerable<OrderType> GetValidMarketOrderTypes();
        Task<IEnumerable<OrderType>> GetValidMarketOrderTypesAsync();
        IEnumerable<OrderType> GetValidOrderTypes();
        Task<IEnumerable<OrderType>> GetValidOrderTypesAsync();
        IEnumerable<CurrencyCode> GetValidPrimaryCurrencyCodes();
        Task<IEnumerable<CurrencyCode>> GetValidPrimaryCurrencyCodesAsync();
        IEnumerable<CurrencyCode> GetValidSecondaryCurrencyCodes();
        Task<IEnumerable<CurrencyCode>> GetValidSecondaryCurrencyCodesAsync();
        IEnumerable<TransactionType> GetValidTransactionTypes();
        Task<IEnumerable<TransactionType>> GetValidTransactionTypesAsync();
        BankOrder PlaceLimitOrder(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, OrderType orderType, decimal price, decimal volume);
        Task<BankOrder> PlaceLimitOrderAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, OrderType orderType, decimal price, decimal volume);
        BankOrder PlaceMarketOrder(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, OrderType orderType, decimal volume);
        Task<BankOrder> PlaceMarketOrderAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, OrderType orderType, decimal volume);
        FiatWithdrawalRequest RequestFiatWithdrawal(CurrencyCode secondaryCurrency, decimal withdrawalAmount, string withdrawalBankAccountName, string comment);
        Task<FiatWithdrawalRequest> RequestFiatWithdrawalAsync(CurrencyCode secondaryCurrency, decimal withdrawalAmount, string withdrawalBankAccountName, string comment);
        BitcoinDepositAddress SynchBitcoinAddressWithBlockchain(string bitcoinAddress);
        Task<BitcoinDepositAddress> SynchBitcoinAddressWithBlockchainAsync(string bitcoinAddress);
        DigitalCurrencyDepositAddress SynchDigitalCurrencyDepositAddressWithBlockchain(string depositAddress, CurrencyCode? primaryCurrency = null);
        Task<DigitalCurrencyDepositAddress> SynchDigitalCurrencyDepositAddressWithBlockchainAsync(string depositAddress, CurrencyCode? primaryCurrency = null);
        void WithdrawBitcoin(decimal? withdrawalAmount, string bitcoinAddress, string comment);
        Task WithdrawBitcoinAsync(decimal? withdrawalAmount, string bitcoinAddress, string comment);
        void WithdrawDigitalCurrency(decimal withdrawalAmount, string withdrawalAddress, string comment, CurrencyCode primaryCurrency);
        Task WithdrawDigitalCurrencyAsync(decimal withdrawalAmount, string withdrawalAddress, string comment, CurrencyCode primaryCurrency);

        void WithdrawDigitalCurrency(DigitalWithdrawalRequest withdrawalRequest);
        Task WithdrawDigitalCurrencyAsync(DigitalWithdrawalRequest withdrawalRequest);
        Task<List<Event>> Events();
    }
}