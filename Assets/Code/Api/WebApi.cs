using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace SMB.Api
{
    internal class WebApi
    {
        internal readonly Dictionary<ApiService, string> KnownServiceUrls = new Dictionary<ApiService, string>()
        {
            { ApiService.Login,         "login/" },
            { ApiService.Register,      "register/" },
            { ApiService.Logout,        "logout/" },
            { ApiService.GameSave,      "gamesave/" },
            { ApiService.Leaderboard,   "leaderboard/" },
            { ApiService.DeleteUser,    "delete_user/" },
            { ApiService.ListUsers,     "list_users/" },
        };

        private static HttpClient _HttpClient;

        private string _BaseApiUrl;

        static WebApi()
        {
            _HttpClient = new HttpClient();
        }

        private WebApi() { }

        /// <summary></summary>
        /// <param name="baseApiUrl">Expected format is: http[s]://[domain].[tld]/[any/path/you/want/]</param>
        internal WebApi(string baseApiUrl)
        {
            // Make the base API URL conform to the expected input format.
            if (!baseApiUrl.EndsWith("/")) baseApiUrl += "/";

            this._BaseApiUrl = baseApiUrl;
        }

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceUrl">Expected format is: [path/to/serviceUrl/] (no leading forward-slash)</param>
        /// <returns></returns>
        internal async Task<T> SendRequestAsync<T>(string serviceUrl)
        {
            // Make the service URL conform to the expected input format.
            if (!serviceUrl.EndsWith("/")) serviceUrl += "/";
            if (serviceUrl.StartsWith("/")) serviceUrl = serviceUrl.Substring(1, serviceUrl.Length - 1);

            // Send the GET request to the specified service URL, and return the deserialized result.
            HttpResponseMessage response = await _HttpClient.GetAsync(_BaseApiUrl + serviceUrl);
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceUrl">Expected format is: [path/to/serviceUrl/] (no leading forward-slash)</param>
        /// <param name="request"></param>
        /// <returns></returns>
        internal async Task<T> SendRequestAsync<T>(string serviceUrl, ApiRequest request)
        {
            // Make the service URL conform to the expected input format.
            if (!serviceUrl.EndsWith("/")) serviceUrl += "/";
            if (serviceUrl.StartsWith("/")) serviceUrl = serviceUrl.Substring(1, serviceUrl.Length - 1);

            // Serialize the request.
            string serializedRequest = JsonConvert.SerializeObject(request);

            // Send the serialized request to the specified service URL.
            HttpResponseMessage response = await _HttpClient.PostAsync(_BaseApiUrl + serviceUrl,
                new StringContent(
                    serializedRequest,
                    Encoding.UTF8,
                    "application/json"
            ));

            // Return the deserialized result.
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}
