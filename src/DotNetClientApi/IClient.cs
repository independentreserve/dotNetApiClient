using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IndependentReserve.DotNetClientApi.Data;
using IndependentReserve.DotNetClientApi.Data.Limits;
using IndependentReserve.DotNetClientApi.Withdrawal;

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

        Task<CancelOrdersResult> CancelOrdersAsync(Guid[] orderGuids);

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
        Task<Page<TradeDetails>> GetTradesByOrder(Guid orderGuid, int pageIndex, int pageSize);
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
        BankOrder PlaceMarketOrder(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, OrderType orderType, decimal volume, CurrencyType? volumeCurrencyType = null);
        Task<BankOrder> PlaceMarketOrderAsync(CurrencyCode primaryCurrency, CurrencyCode secondaryCurrency, OrderType orderType, decimal volume, CurrencyType? volumeCurrencyType = null);

        Task<IEnumerable<FiatBankAccount>> GetFiatBankAccountsAsync();
        FiatWithdrawalRequest RequestFiatWithdrawal(CurrencyCode secondaryCurrency, decimal withdrawalAmount, string withdrawalBankAccountName, string comment);
        Task<FiatWithdrawalRequest> RequestFiatWithdrawalAsync(CurrencyCode secondaryCurrency, decimal withdrawalAmount, string withdrawalBankAccountName, string comment);
        Task<FiatWithdrawalRequest> WithdrawFiatCurrencyAsync(CurrencyCode secondaryCurrency, decimal withdrawalAmount, Guid bankAccountGuid, bool useNpp, string comment);
        
        BitcoinDepositAddress SynchBitcoinAddressWithBlockchain(string bitcoinAddress);
        Task<BitcoinDepositAddress> SynchBitcoinAddressWithBlockchainAsync(string bitcoinAddress);
        DigitalCurrencyDepositAddress SynchDigitalCurrencyDepositAddressWithBlockchain(string depositAddress, CurrencyCode primaryCurrency);
        Task<DigitalCurrencyDepositAddress> SynchDigitalCurrencyDepositAddressWithBlockchainAsync(string depositAddress, CurrencyCode primaryCurrency);
        void WithdrawBitcoin(decimal? withdrawalAmount, string bitcoinAddress, string comment);
        Task WithdrawBitcoinAsync(decimal? withdrawalAmount, string bitcoinAddress, string comment);
        CryptoWithdrawal WithdrawDigitalCurrency(decimal withdrawalAmount, string withdrawalAddress, string comment, CurrencyCode primaryCurrency);
        Task<CryptoWithdrawal> WithdrawDigitalCurrencyAsync(decimal withdrawalAmount, string withdrawalAddress, string comment, CurrencyCode primaryCurrency);

        CryptoWithdrawal WithdrawDigitalCurrency(DigitalWithdrawalRequest withdrawalRequest);
        Task<CryptoWithdrawal> WithdrawDigitalCurrencyAsync(DigitalWithdrawalRequest withdrawalRequest);
        Task<CryptoWithdrawal> GetDigitalCurrencyWithdrawalAsync(Guid transactionGuid);

        Task<List<Event>> GetEvents();
        Task<ExchangeStatus> GetExchangeStatus();
        
        Task<IEnumerable<WithdrawalFee>> GetFiatWithdrawalFees();
        Task<IEnumerable<DepositFee>> GetDepositFees();
        Task<Dictionary<CurrencyCode, decimal>> GetOrderMinimumVolumes();

        Task<DepositLimits> GetDepositLimits();
        Task<Dictionary<string, List<WithdrawalLimit>>> GetWithdrawalLimits();
        Task<Dictionary<CurrencyCode, decimal>> GetCryptoWithdrawalFees();
    }
}