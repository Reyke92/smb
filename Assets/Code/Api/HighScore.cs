using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SMB.Api
{
    [Serializable]
    internal class HighScore
    {
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        internal string Username { get; set; }

        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        internal long Score { get; set; }
    }
}
