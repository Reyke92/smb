using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMB.Api
{
    [Serializable]
    internal class AuthedApiRequest : ApiRequest
    {
        [JsonProperty("session", NullValueHandling = NullValueHandling.Ignore)]
        internal string Session { get; set; }
    }
}
