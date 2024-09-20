using Newtonsoft.Json;

namespace BTCPayServer.Services.Altcoins.Nano.RPC.Models
{
    public partial class VersionRequest : INanoRequest
    {
        [JsonProperty("action")]
        public string Action
        {
            get { return "version"; }
        }
    }
}

