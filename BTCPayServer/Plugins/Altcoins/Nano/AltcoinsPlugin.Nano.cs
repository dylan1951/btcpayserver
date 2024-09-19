using System;
using System.Linq;
using BTCPayServer.Configuration;
using BTCPayServer.Hosting;
using BTCPayServer.Payments;
using BTCPayServer.Payments.Bitcoin;
using BTCPayServer.Services.Altcoins.Nano.Configuration;
using BTCPayServer.Services.Altcoins.Nano.Payments;
using BTCPayServer.Services.Altcoins.Nano.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using BTCPayServer.Abstractions.Contracts;
using BTCPayServer.Abstractions.Services;

namespace BTCPayServer.Plugins.Altcoins;

public partial class AltcoinsPlugin
{
    public void InitNano(IServiceCollection services)
    {
        var network = new NanoLikeSpecificBtcPayNetwork
        {
            CryptoCode = "XNO",
            DisplayName = "Nano",
            Divisibility = 30,
            DefaultRateRules = new[]
            {
                "XNO_X = XNO_BTC * BTC_X",
                "XNO_BTC = kraken(NANO_BTC)"
            },
            CryptoImagePath = "/imlegacy/nano.png",
            UriScheme = "nano"
        };
        
        const string blockExplorerLink = "https://nanexplorer.com/nano/block/{0}";
        var pmi = PaymentTypes.CHAIN.GetPaymentMethodId("XNO");
        
        services.AddBTCPayNetwork(network).AddTransactionLinkProvider(network.CryptoCode, new SimpleTransactionLinkProvider(blockExplorerLink));
        services.AddSingleton(provider => (IPaymentMethodViewExtension)ActivatorUtilities.CreateInstance(provider, typeof(BitcoinPaymentMethodViewExtension), pmi));

        services.AddSingleton(provider => ConfigureNanoLikeConfiguration(provider));
        
        services.AddSingleton<NanoRPCProvider>();
        services.AddHostedService<NanoLikeSummaryUpdaterHostedService>();
        services.AddHostedService<NanoListener>();
        
        services.AddSingleton(provider => (IPaymentMethodHandler)ActivatorUtilities.CreateInstance(provider, typeof(NanoLikePaymentMethodHandler), network));
        services.AddSingleton(provider => (IPaymentLinkExtension)ActivatorUtilities.CreateInstance(provider, typeof(NanoPaymentLinkExtension), network, pmi));
        services.AddSingleton(provider => (IPaymentModelExtension)ActivatorUtilities.CreateInstance(provider, typeof(NanoPaymentModelExtension), network, pmi));
        
        services.AddSingleton<IUIExtension>(new UIExtension("Nano/StoreNavNanoExtension", "store-nav"));
        services.AddSingleton<IUIExtension>(new UIExtension("Nano/StoreWalletsNavNanoExtension", "store-wallets-nav"));
        services.AddSingleton<IUIExtension>(new UIExtension("Nano/ViewNanoLikePaymentData", "store-invoices-payments"));
        services.AddSingleton<ISyncSummaryProvider, NanoSyncSummaryProvider>();
    }

    static NanoLikeConfiguration ConfigureNanoLikeConfiguration(IServiceProvider serviceProvider)
    {
        var configuration = serviceProvider.GetService<IConfiguration>();
        var btcPayNetworkProvider = serviceProvider.GetService<BTCPayNetworkProvider>();
        var result = new NanoLikeConfiguration();

        var supportedNetworks = btcPayNetworkProvider.GetAll().OfType<NanoLikeSpecificBtcPayNetwork>();

        foreach (var nanoLikeSpecificBtcPayNetwork in supportedNetworks)
        {
            var daemonUri =
                configuration.GetOrDefault<Uri>(
                    $"{nanoLikeSpecificBtcPayNetwork.CryptoCode}_pippin_uri", null);
            if (daemonUri == null)
            {
                throw new ConfigException($"{nanoLikeSpecificBtcPayNetwork.CryptoCode} is misconfigured");
            }

            result.NanoLikeConfigurationItems.Add(nanoLikeSpecificBtcPayNetwork.CryptoCode, new NanoLikeConfigurationItem()
            {
                DaemonRpcUri = daemonUri
            });
        }
        return result;
    }
}
