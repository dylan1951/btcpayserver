using System;
using System.Collections.Generic;

namespace BTCPayServer.Services.Altcoins.Nano.Configuration
{
    public class NanoLikeConfiguration
    {
        public Dictionary<string, NanoLikeConfigurationItem> NanoLikeConfigurationItems { get; set; } =
            new Dictionary<string, NanoLikeConfigurationItem>();
    }

    public class NanoLikeConfigurationItem
    {
        public Uri DaemonRpcUri { get; set; }
    }
}
