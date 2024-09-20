using Newtonsoft.Json;

namespace BTCPayServer.Services.Altcoins.Nano.RPC.Models
{
    public partial class VersionResponse
    {
        [JsonProperty("rpc_version")] public string RpcVersion { get; set; }
    }
}
