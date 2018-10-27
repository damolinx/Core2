using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Core2
{
    public class ConfigurationBase
    {
        [JsonExtensionData]
        protected IDictionary<string, JToken> AdditionalData;
    }
}
