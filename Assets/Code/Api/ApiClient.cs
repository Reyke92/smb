using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMB.Api
{
    internal class ApiClient
    {
        private const string _BASE_API_URL_PROD = "https://nobytes.me/smb_api/";

        internal bool IsAdmin { get; private set; }
        internal bool IsLoggedIn
        {
            get
            {
                return _SessionKey != null && _SessionKey.Length != 0;
            }
        }
        internal string Username { get; private set; }

        private WebApi _Api;
        private readonly string _BaseApiUrl;
        private string _SessionKey;

        internal ApiClient()
        {
            _BaseApiUrl = _BASE_API_URL_PROD;
            _Api = new WebApi(_BaseApiUrl);
        }

        internal ApiClient(string customBaseApiUrl)
        {
            _BaseApiUrl = customBaseApiUrl;
            _Api = new WebApi(_BaseApiUrl);
        }

        internal async Task<ApiLoginResponse> LoginAsync(string username, string password)
        {
            // Make sure we are logged out when we call this method.
            if (IsLoggedIn) throw new LogoutRequiredException();

            ApiLoginRequest request = new ApiLoginRequest()
            {
                Username = username,
                Password = password
            };

            ApiLoginResponse response = await _Api.SendRequestAsync<ApiLoginResponse>(
                _Api.KnownServiceUrls[ApiService.Login],
                request
            );

            // If the login was successful.
            if (response.Error == ApiErrorCode.NoError)
            {
                this.Username = username;
                this.IsAdmin = response.IsAdmin;
                this._SessionKey = response.SessionKey;
            }

            return response;
        }

        internal async Task<ApiRegisterResponse> RegisterAsync(string username, string password)
        {
            // Make sure we are logged out when we call this method.
            if (IsLoggedIn) throw new LogoutRequiredException();

            ApiRegisterRequest request = new ApiRegisterRequest()
            {
                Username = username,
                Password = password
            };

            ApiRegisterResponse response = await _Api.SendRequestAsync<ApiRegisterResponse>(
                _Api.KnownServiceUrls[ApiService.Register],
                request
            );

            // If the registration was successful.
            if (response.Error == ApiErrorCode.NoError)
            {
                this.Username = username;
                this.IsAdmin = response.IsAdmin;
                this._SessionKey = response.SessionKey;
            }

            return response;
        }

        internal async Task<ApiLogoutResponse> LogoutAsync()
        {
            // Make sure we are logged in when we call this method.
            if (!IsLoggedIn) throw new LoginRequiredException();

            ApiLogoutRequest request = new ApiLogoutRequest()
            {
                Session = _SessionKey
            };

            ApiLogoutResponse response = await _Api.SendRequestAsync<ApiLogoutResponse>(
                _Api.KnownServiceUrls[ApiService.Logout],
                request
            );

            // Clear our session key, username, and other stored info after sending a logout request.
            this._SessionKey = ""; 
            this.Username = "";
            this.IsAdmin = false;

            return response;
        }

        internal async Task<ApiLeaderboardResponse> GetLeaderboardRankingsAsync()
        {
            // -- No current session is needed for this method to be called.

            return await _Api.SendRequestAsync<ApiLeaderboardResponse>(
                _Api.KnownServiceUrls[ApiService.Leaderboard]
            );
        }

        internal async Task<ApiGameSaveResponse> DownloadGameSaveAsync()
        {
            // Make sure we are logged in when we call this method.
            if (!IsLoggedIn) throw new LoginRequiredException();

            ApiGameSaveRequest request = new ApiGameSaveRequest()
            {
                Session = _SessionKey,
                Action = ApiGameSaveAction.Retrieve
            };

            return await _Api.SendRequestAsync<ApiGameSaveResponse>(
                _Api.KnownServiceUrls[ApiService.GameSave],
                request
            );
        }

        internal async Task<ApiGameSaveResponse> UploadGameSaveAsync(GameSave gameSave)
        {
            // Make sure we are logged in when we call this method.
            if (!IsLoggedIn) throw new LoginRequiredException();

            ApiGameSaveRequest request = new ApiGameSaveRequest()
            {
                Session = _SessionKey,
                Action = ApiGameSaveAction.Save,
                GameSave = gameSave
            };

            return await _Api.SendRequestAsync<ApiGameSaveResponse>(
                _Api.KnownServiceUrls[ApiService.GameSave],
                request
            );
        }

        internal async Task<ApiDeleteUserResponse> DeleteUserAsync(string username)
        {
            // Make sure we are logged in when we call this method.
            // Also make sure that we have ADMIN privileges before calling this method.
            if (!IsLoggedIn) throw new LoginRequiredException();
            else if (!IsAdmin) throw new NotAuthorizedException();

            ApiDeleteUserRequest request = new ApiDeleteUserRequest()
            {
                Session = _SessionKey,
                Username = username
            };

            return await _Api.SendRequestAsync<ApiDeleteUserResponse>(
                _Api.KnownServiceUrls[ApiService.DeleteUser],
                request
            );
        }

        internal async Task<ApiListUsersResponse> DownloadUserListAsync()
        {
            // Make sure we are logged in when we call this method.
            // Also make sure that we have ADMIN privileges before calling this method.
            if (!IsLoggedIn) throw new LoginRequiredException();
            else if (!IsAdmin) throw new NotAuthorizedException();

            ApiDeleteUserRequest request = new ApiDeleteUserRequest()
            {
                Session = _SessionKey
            };

            return await _Api.SendRequestAsync<ApiListUsersResponse>(
                _Api.KnownServiceUrls[ApiService.ListUsers],
                request
            );
        }
    }
}
