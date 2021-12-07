using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMB.Api
{
    [Serializable]
    internal class ApiGameSaveRequest : AuthedApiRequest
    {
        [JsonProperty("action", NullValueHandling = NullValueHandling.Ignore)]
        internal ApiGameSaveAction Action { get; set; }

        [JsonProperty("gameSave", NullValueHandling = NullValueHandling.Ignore)]
        internal GameSave GameSave { get; set; }
    }
}
