using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMB.Api
{
    [Serializable]
    internal class ApiLoginRequest : ApiRequest
    {
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        internal string Username { get; set; }

        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        internal string Password { get; set; }
    }
}
