using Newtonsoft.Json;

namespace BTCPayServer.Services.Altcoins.Nano.RPC.Models
{
    public partial class AccountCreateResponse
    {
        [JsonProperty("account")] public string Account { get; set; }
    }
}
