using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMB.Api
{
    [Serializable]
    internal class ApiRegisterResponse : ApiResponse
    {
        [JsonProperty("session", NullValueHandling = NullValueHandling.Ignore)]
        internal string SessionKey { get; set; }

        [JsonProperty("isAdmin", NullValueHandling = NullValueHandling.Ignore)]
        internal bool IsAdmin { get; set; }
    }
}
