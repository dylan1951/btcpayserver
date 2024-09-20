using BTCPayServer.Client.Models;
using BTCPayServer.Payments;
using BTCPayServer.Plugins.Altcoins;
using BTCPayServer.Services.Altcoins.Nano.Utils;
using BTCPayServer.Services.Invoices;

namespace BTCPayServer.Services.Altcoins.Nano.Payments
{
    public class NanoLikePaymentData : CryptoPaymentData
    {
        public string TransactionHash { get; set; }
        public string GetPaymentProof()
        {
            return null;
        }
    }
}
