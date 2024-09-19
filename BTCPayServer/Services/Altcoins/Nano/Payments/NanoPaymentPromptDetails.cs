using BTCPayServer.Payments;

namespace BTCPayServer.Services.Altcoins.Nano.Payments
{
    public class NanoPaymentPromptDetails
    {
        public long AccountIndex { get; set; }
        public long AddressIndex { get; set; }
        public string DepositAddress { get; set; }
    }
}
