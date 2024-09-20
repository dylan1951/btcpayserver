using System;
using System.Collections.Generic;

namespace BTCPayServer.Services.Altcoins.Nano.Configuration
{
    public class NanoLikeConfiguration
    {
        public Dictionary<string, NanoLikeConfigurationItem> NanoLikeConfigurationItems { get; set; } = new();
    }

    public class NanoLikeConfigurationItem
    {
        public Uri PippinUri { get; set; }
    }
}
