namespace BTCPayServer.Services.Altcoins.Nano.RPC
{
    public class NanoEvent
    {
        public string BlockHash { get; set; }
        public string TransactionHash { get; set; }
        public string CryptoCode { get; set; }

        public override string ToString()
        {
            return
                $"{CryptoCode}: {(string.IsNullOrEmpty(TransactionHash) ? string.Empty : "Tx Update")}{(string.IsNullOrEmpty(BlockHash) ? string.Empty : "New Block")} ({TransactionHash ?? string.Empty}{BlockHash ?? string.Empty})";
        }
    }
}
