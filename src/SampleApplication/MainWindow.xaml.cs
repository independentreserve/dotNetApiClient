using System;
using System.Configuration;
using System.Windows;
using IndependentReserve.DotNetClientApi;
using Newtonsoft.Json;
using SampleApplication.ViewModels;

namespace SampleApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["apiKey"]) || string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["apiUrl"]) || string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["apiSecret"]))
            {
                MessageBoxResult result = MessageBox.Show("Some or all of the following required API key details are empty: ApiKey, ApiUrl, ApiSecret.\n\nPlease set them to the correct values in application configuration file.", "Please specify your API key details", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return;
            }

            this.DataContext = new AppViewModel()
            {
                SelectedMethod = MethodMetadata.Null,
                ApiKey = ConfigurationManager.AppSettings["apiKey"],
                ApiUrl = ConfigurationManager.AppSettings["apiUrl"]
            };
        }

        private AppViewModel ViewModel
        {
            get { return (AppViewModel) this.DataContext; }
        }

        /// <summary>
        /// Handles CallIt button click; calls seleted API method with specified parameters, shows what API URL was requested, using what HTTP method, and what was parameters and response
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CallIt_OnClick(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedMethod == MethodMetadata.Null)
            {
                return;
            }

            string apiKey = ConfigurationManager.AppSettings["apiKey"];
            string apiSecret =ConfigurationManager.AppSettings["apiSecret"];
            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];

            //create instane of API client capable to call both private and public api methods
            using (var client = Client.CreatePrivate(apiKey, apiSecret, apiUrl))
            {
                ViewModel.LastRequestParameters = string.Empty;
                ViewModel.LastRequestResponse = string.Empty;
                ViewModel.LastRequestUrl = string.Empty;

                try
                {
                    //call selected method with current parameters
                    if (ViewModel.SelectedMethod == MethodMetadata.GetValidPrimaryCurrencyCodes)
                    {
                        await client.GetValidPrimaryCurrencyCodesAsync();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetValidSecondaryCurrencyCodes)
                    {
                        await client.GetValidSecondaryCurrencyCodesAsync();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetValidLimitOrderTypes)
                    {
                        await client.GetValidLimitOrderTypesAsync();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetValidMarketOrderTypes)
                    {
                        await client.GetValidMarketOrderTypesAsync();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetMarketSummary)
                    {
                        await client.GetMarketSummaryAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetOrderBook)
                    {
                        await client.GetOrderBookAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetTradeHistorySummary)
                    {
                        await client.GetTradeHistorySummaryAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency, ViewModel.NumberOfHoursInThePastToRetrieve??0);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetRecentTrades)
                    {
                        await client.GetRecentTradesAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency, ViewModel.NumberOfRecentTradesToRetrieve??0);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.PlaceLimitOrder)
                    {
                        await client.PlaceLimitOrderAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency, ViewModel.LimitOrderType, ViewModel.LimitOrderPrice ?? 0, ViewModel.OrderVolume ?? 0);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.PlaceMarketOrder)
                    {
                        await client.PlaceMarketOrderAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency, ViewModel.MarketOrderType, ViewModel.OrderVolume ?? 0);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.CancelOrder)
                    {
                        await client.CancelOrderAsync(ParseGuid(ViewModel.OrderGuid));
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetAccounts)
                    {
                        await client.GetAccountsAsync();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetOpenOrders)
                    {
                        await client.GetOpenOrdersAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency, ViewModel.PageIndex ?? 0, ViewModel.PageSize ?? 0);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetClosedOrders)
                    {
                        await client.GetClosedOrdersAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency, ViewModel.PageIndex ?? 0, ViewModel.PageSize ?? 0);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetClosedFilledOrders)
                    {
                        await client.GetClosedFilledOrdersAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency, ViewModel.PageIndex ?? 0, ViewModel.PageSize ?? 0);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetOrderDetails)
                    {
                        await client.GetOrderDetailsAsync(ParseGuid(ViewModel.OrderGuid));
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetTransactions)
                    {
                        await client.GetTransactionsAsync(ParseGuid(ViewModel.AccountGuid), ViewModel.FromTimestampUtc, ViewModel.ToTimestampUtc, ViewModel.PageIndex ?? 0, ViewModel.PageSize ?? 0);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetBitcoinDepositAddress)
                    {
                        await client.GetBitcoinDepositAddressAsync();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.RequestFiatWithdrawal)
                    {
                        await client.RequestFiatWithdrawalAsync(ViewModel.SecondaryCurrency, ViewModel.WithdrawalAmount, ViewModel.WithdrawalBankAccountName);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.SynchBitcoinAddressWithBlockchain)
                    {
                        await client.SynchBitcoinAddressWithBlockchainAsync(ViewModel.Address);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.WithdrawBitcoin)
                    {
                        await client.WithdrawBitcoinAsync(ViewModel.WithdrawalAmount, ViewModel.Address);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetTrades)
                    {
                        await client.GetTradesAsync(ViewModel.PageIndex ?? 0, ViewModel.PageSize ?? 0);
                    }

                    ViewModel.LastRequestResponse = FormatJson(client.LastResponseRaw);
                }
                catch (Exception ex)
                {
                    ViewModel.LastRequestResponse = ex.Message;
                }

                ViewModel.LastRequestUrl = string.Format("{0} {1}", client.LastRequestHttpMethod, client.LastRequestUrl);
                ViewModel.LastRequestParameters = client.LastRequestHttpMethod == "GET" ? (
                    
                    string.IsNullOrWhiteSpace(client.LastRequestParameters)?"no parameters": client.LastRequestParameters ) : 
                    
                    
                    (string.IsNullOrWhiteSpace(client.LastRequestParameters) ? "no parameters" : FormatJson(client.LastRequestParameters));
            }
        }

        /// <summary>
        /// Helper method to format JSON string to the nice ideneted format
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string FormatJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);

            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }

        private static Guid ParseGuid(string guidString)
        {
            Guid guid;
            if (Guid.TryParse(guidString, out guid))
            {
                return guid;
            }

            return Guid.Empty;
        }
    }
}
