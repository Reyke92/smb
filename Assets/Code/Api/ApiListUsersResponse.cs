using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SMB.Api
{
    [Serializable]
    internal class ApiListUsersResponse : ApiResponse
    {
        [JsonProperty("users", NullValueHandling = NullValueHandling.Ignore)]
        internal string[] Users { get; set; }
    }
}
