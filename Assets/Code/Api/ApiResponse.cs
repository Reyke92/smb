using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SMB.Api
{
    [Serializable]
    internal class ApiResponse
    {
        [JsonProperty("error", Required = Required.Always)]
        internal ApiErrorCode Error { get; set; }
    }
}
