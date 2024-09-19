using Newtonsoft.Json;

namespace BTCPayServer.Services.Altcoins.Nano.RPC.Models
{
    public partial class MakeUriResponse
    {
        [JsonProperty("uri")] public string Uri { get; set; }
    }
}
