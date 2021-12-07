using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMB.Api
{
    [Serializable]
    internal class ApiLeaderboardResponse : ApiResponse
    {
        [JsonProperty("rankings", NullValueHandling = NullValueHandling.Ignore)]
        internal HighScore[] Rankings { get; set; }
    }
}
