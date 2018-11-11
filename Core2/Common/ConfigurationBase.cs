using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Core2.Common
{
    public abstract class ConfigurationBase
    {
        protected ConfigurationBase()
        {
        }

        [JsonExtensionData]
        protected IDictionary<string, JToken> AdditionalData;
    }
}
