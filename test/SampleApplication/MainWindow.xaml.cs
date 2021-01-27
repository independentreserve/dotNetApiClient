﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Windows;
using IndependentReserve.DotNetClientApi;
using IndependentReserve.DotNetClientApi.Data;
using Newtonsoft.Json;
using NLog;
using SampleApplication.ViewModels;

namespace SampleApplication
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();

            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) =>
            {
                // [Environment]::SetEnvironmentVariable("IR_DOTNETCLIENTAPI_IGNORE_SSL_ERROR", "true", "Machine")
                const string envKeyIgnoreSsl = "IR_DOTNETCLIENTAPI_IGNORE_SSL_ERROR";
                var ignoreSslText = Environment.GetEnvironmentVariable(envKeyIgnoreSsl);

                var parsedOk = bool.TryParse(ignoreSslText, out var ignoreSslValue);

                if (parsedOk && ignoreSslValue)
                {
                    return true;
                }

                if (sslPolicyErrors != SslPolicyErrors.None)
                {
                    Log.Info($"SSL Certificate error ({sslPolicyErrors}). Ignore using $env:{envKeyIgnoreSsl}");
                    return false;
                }

                return true;
            };

            Log.Info("Reading credentials");
            var config = new AppSettingsConfigProvider().Get();

            if (!config.HasCredential)
            {
                config = new EnvironmentVariableConfigProvider().Get();
            }

            if (!config.HasCredential)
            {
                MessageBox.Show(
                    "Some or all of the following required API key details are empty: ApiKey, ApiUrl, ApiSecret.\n\nPlease set them to the correct values in application configuration file."
                    , "Please specify your API key details"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Error);

                Application.Current.Shutdown();
                return;
            }

            Log.Info("Setting DataContext");
            DataContext = new AppViewModel(config)
            {
                SelectedMethod = MethodMetadata.Null
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

            var viewModel = (AppViewModel) this.DataContext;

            //create instane of API client capable to call both private and public api methods
            using (var client = Client.Create(viewModel.ApiConfig))
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
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetValidOrderTypes)
                    {
                        await client.GetValidOrderTypesAsync();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetValidTransactionTypes)
                    {
                        await client.GetValidTransactionTypesAsync();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetMarketSummary)
                    {
                        await client.GetMarketSummaryAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetOrderBook)
                    {
                        await client.GetOrderBookAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetAllOrders)
                    {
                        await client.GetAllOrdersAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetTradeHistorySummary)
                    {
                        await client.GetTradeHistorySummaryAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency, ViewModel.NumberOfHoursInThePastToRetrieve??0);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetRecentTrades)
                    {
                        await client.GetRecentTradesAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency, ViewModel.NumberOfRecentTradesToRetrieve??0);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetFxRates)
                    {
                        await client.GetFxRatesAsync();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.PlaceLimitOrder)
                    {
                        await client.PlaceLimitOrderAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency, ViewModel.LimitOrderType, ViewModel.LimitOrderPrice ?? 0, ViewModel.OrderVolume ?? 0);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.PlaceMarketOrder)
                    {
                        await client.PlaceMarketOrderAsync(ViewModel.PrimaryCurrency, ViewModel.SecondaryCurrency, ViewModel.MarketOrderType, ViewModel.OrderVolume ?? 0, ViewModel.VolumeCurrencyType);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.CancelOrder)
                    {
                        await client.CancelOrderAsync(ParseGuid(ViewModel.OrderGuid));
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetAccounts)
                    {
                        await client.GetAccountsAsync();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetBrokerageFees)
                    {
                        await client.GetBrokerageFeesAsync();
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
                        var transactionTypes = (from transactionTypeViewModel in ViewModel.TransactionTypes where transactionTypeViewModel.IsSelected select transactionTypeViewModel.Type.ToString()).ToArray();
                        await client.GetTransactionsAsync(ParseGuid(ViewModel.AccountGuid), ViewModel.FromTimestampUtc, ViewModel.ToTimestampUtc, transactionTypes, ViewModel.PageIndex ?? 0, ViewModel.PageSize ?? 0);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetDigitalCurrencyDepositAddress)
                    {
                        await client.GetDigitalCurrencyDepositAddressAsync(ViewModel.PrimaryCurrency);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetDigitalCurrencyDepositAddresses)
                    {
                        await client.GetDigitalCurrencyDepositAddressesAsync(ViewModel.PrimaryCurrency, ViewModel.PageIndex ?? 0, ViewModel.PageSize ?? 0);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetFiatBankAccounts)
                    {
                        await client.GetFiatBankAccountsAsync();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.RequestFiatWithdrawal)
                    {
                        await client.RequestFiatWithdrawalAsync(ViewModel.SecondaryCurrency, ViewModel.WithdrawalAmount, ViewModel.WithdrawalBankAccountName, ViewModel.Comment);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.WithdrawFiatCurrency)
                    {
                        await client.WithdrawFiatCurrencyAsync(ViewModel.SecondaryCurrency, ViewModel.WithdrawalAmount, ParseGuid(ViewModel.BankAccountGuid), ViewModel.UseNpp, ViewModel.Comment);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetFiatWithdrawal)
                    {
                        await client.GetFiatWithdrawalAsync(ParseGuid(ViewModel.TransactionGuid));
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.SynchDigitalCurrencyDepositAddressWithBlockchain)
                    {
                        await client.SynchDigitalCurrencyDepositAddressWithBlockchainAsync(ViewModel.Address, ViewModel.PrimaryCurrency);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.WithdrawDigitalCurrency)
                    {
                        await client.WithdrawDigitalCurrencyAsync(new DigitalWithdrawalRequest
                        {
                            Amount = ViewModel.WithdrawalAmount,
                            Address = ViewModel.Address,
                            Comment = ViewModel.Comment,
                            Currency = ViewModel.PrimaryCurrency,
                            DestinationTag = string.IsNullOrWhiteSpace(ViewModel.Tag) ? null : ViewModel.Tag,
                        });
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetDigitalCurrencyWithdrawal)
                    {
                        await client.GetDigitalCurrencyWithdrawalAsync(ParseGuid(ViewModel.TransactionGuid));
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetTrades)
                    {
                        await client.GetTradesAsync(ViewModel.PageIndex ?? 0, ViewModel.PageSize ?? 0);
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetTrades2)
                    {
                        await client.GetTrades2Async(ParseGuid(ViewModel.OrderGuid));
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetEvents)
                    {
                        await client.GetEvents();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetExchangeStatus)
                    {
                        await client.GetExchangeStatus();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetWithdrawalFees)
                    {
                        await client.GetWithdrawalFees();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetDepositFees)
                    {
                        await client.GetDepositFees();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetDepositLimits)
                    {
                        await client.GetDepositLimits();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetWithdrawalLimits)
                    {
                        await client.GetWithdrawalLimits();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetOrderMinimumVolumes)
                    {
                        await client.GetOrderMinimumVolumes();
                    }
                    else if (ViewModel.SelectedMethod == MethodMetadata.GetCryptoWithdrawalFees)
                    {
                        await client.GetCryptoWithdrawalFees();
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
        /// Helper method to format JSON string to the nice indented format
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string FormatJson(string json)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                DateParseHandling = DateParseHandling.DateTimeOffset
            };

            dynamic parsedJson = JsonConvert.DeserializeObject(json, serializerSettings);

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
