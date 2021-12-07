using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMB.Api
{
    [Serializable]
    internal class ApiGameSaveResponse : ApiResponse
    {
        [JsonProperty("gameSave", NullValueHandling = NullValueHandling.Ignore)]
        internal GameSave GameSave { get; set; }
    }
}
