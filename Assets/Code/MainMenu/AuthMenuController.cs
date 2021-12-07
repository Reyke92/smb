using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using SMB.Api;
using UnityEngine.SceneManagement;

namespace SMB.MainMenu
{
    public class AuthMenuController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _LoginPanel;
        [SerializeField]
        private Button _LPBackButton;
        [SerializeField]
        private InputField _LPUsernameBox;
        [SerializeField]
        private InputField _LPPasswordBox;
        [SerializeField]
        private Button _LPLoginButton;

        [SerializeField]
        private GameObject _RegisterPanel;
        [SerializeField]
        private Button _RPBackButton;
        [SerializeField]
        private InputField _RPUsernameBox;
        [SerializeField]
        private InputField _RPPasswordBox;
        [SerializeField]
        private Button _RPRegisterButton;

        [SerializeField]
        private GameObject _MainPanel;
        [SerializeField]
        private Button _MPRegisterButton;
        [SerializeField]
        private Button _MPLoginButton;

        [SerializeField]
        private Button _ExitButton;

        private void Start()
        {
            // Add UI event listeners.
            _RPUsernameBox.onValueChanged.AddListener(_OnRPUsernameChanged);
            _RPPasswordBox.onValueChanged.AddListener(_OnRPPasswordChanged);
            _LPUsernameBox.onValueChanged.AddListener(_OnLPUsernameChanged);
            _LPPasswordBox.onValueChanged.AddListener(_OnLPPasswordChanged);
            _RPBackButton.onClick.AddListener(_OnBackButtonClicked);
            _LPBackButton.onClick.AddListener(_OnBackButtonClicked);
            _ExitButton.onClick.AddListener(_OnExitButtonClicked);
            _RPRegisterButton.onClick.AddListener(_OnRPRegisterButtonClicked);
            _LPLoginButton.onClick.AddListener(_OnLPLoginButtonClicked);
            _MPRegisterButton.onClick.AddListener(_OnMPRegisterButtonClicked);
            _MPLoginButton.onClick.AddListener(_OnMPLoginButtonClicked);

            // Only show Main Panel when the user is shown the authentication screen.
            _ShowMainPanel();
        }

        private void _OnRPUsernameChanged(string username)
        {
            string restrictedUsername = _RestrictInputToAlphanumericChars(username);
            if (username != restrictedUsername) _RPUsernameBox.text = restrictedUsername;

            if (_RPUsernameBox.text.Length > 0 && _RPPasswordBox.text.Length > 0)
            {
                _RPRegisterButton.gameObject.SetActive(true);
            }
            else _RPRegisterButton.gameObject.SetActive(false);

        }
        private void _OnRPPasswordChanged(string password)
        {
            string restrictedPassword = _RestrictInputToAlphanumericChars(password);
            if (password != restrictedPassword) _RPPasswordBox.text = restrictedPassword;

            if (_RPUsernameBox.text.Length > 0 && _RPPasswordBox.text.Length > 0)
            {
                _RPRegisterButton.gameObject.SetActive(true);
            }
            else _RPRegisterButton.gameObject.SetActive(false);
        }
        private void _OnRPRegisterButtonClicked()
        {
            _RPUsernameBox.enabled = true;
            _RPPasswordBox.enabled = true;
            _RPRegisterButton.enabled = true;
            _RPBackButton.enabled = true;
            _RegisterAsync();
        }

        private void _OnLPUsernameChanged(string username)
        {
            string restrictedUsername = _RestrictInputToAlphanumericChars(username);
            if (username != restrictedUsername) _LPUsernameBox.text = restrictedUsername;

            if (_LPUsernameBox.text.Length > 0 && _LPPasswordBox.text.Length > 0)
            {
                _LPLoginButton.gameObject.SetActive(true);
            }
            else _LPLoginButton.gameObject.SetActive(false);

        }
        private void _OnLPPasswordChanged(string password)
        {
            string restrictedPassword = _RestrictInputToAlphanumericChars(password);
            if (password != restrictedPassword) _LPPasswordBox.text = restrictedPassword;

            if (_LPUsernameBox.text.Length > 0 && _LPPasswordBox.text.Length > 0)
            {
                _LPLoginButton.gameObject.SetActive(true);
            }
            else _LPLoginButton.gameObject.SetActive(false);
        }
        private void _OnLPLoginButtonClicked()
        {
            _LPUsernameBox.enabled = false;
            _LPPasswordBox.enabled = false;
            _LPLoginButton.enabled = false;
            _LPBackButton.enabled = false;
            _LoginAsync();
        }

        private void _OnMPRegisterButtonClicked()
        {
            _ShowRegisterPanel();
        }
        private void _OnMPLoginButtonClicked()
        {
            _ShowLoginPanel();
        }
        private void _OnBackButtonClicked()
        {
            _ShowMainPanel();
        }

        private void _OnExitButtonClicked()
        {
            Application.Quit();
        }

        private void _ShowLoginPanel()
        {
            _RegisterPanel.SetActive(false);
            _MainPanel.SetActive(false);
            _LoginPanel.SetActive(true);
        }
        private void _ShowRegisterPanel()
        {
            _RegisterPanel.SetActive(true);
            _MainPanel.SetActive(false);
            _LoginPanel.SetActive(false);
        }
        private void _ShowMainPanel()
        {
            _RegisterPanel.SetActive(false);
            _MainPanel.SetActive(true);
            _LoginPanel.SetActive(false);
        }

        private async void _LoginAsync()
        {
            ApiLoginResponse response = await SessionManager.ApiClient.LoginAsync(_LPUsernameBox.text, _LPPasswordBox.text);

            if (response.Error == ApiErrorCode.NoError)
            {
                SceneManager.LoadScene("MainMenuScene");
            }
            else
            {
                PopupBoxManager.Instance.CreatePopup("Login was unsuccessful. Reason: " + response.Error.ToString());
                _LPUsernameBox.enabled = true;
                _LPPasswordBox.enabled = true;
                _LPLoginButton.enabled = true;
                _LPBackButton.enabled = true;
            }
        }

        private async void _RegisterAsync()
        {
            ApiRegisterResponse response = await SessionManager.ApiClient.RegisterAsync(_RPUsernameBox.text, _RPPasswordBox.text);

            if (response.Error == ApiErrorCode.NoError)
            {
                SceneManager.LoadScene("MainMenuScene");
            }
            else
            {
                PopupBoxManager.Instance.CreatePopup("Registration was unsuccessful. Reason: " + response.Error.ToString());
                _RPUsernameBox.enabled = true;
                _RPPasswordBox.enabled = true;
                _RPRegisterButton.enabled = true;
                _RPBackButton.enabled = true;
            }
        }

        private string _RestrictInputToAlphanumericChars(string input)
        {
            // Only allow alphanumeric input to pass through into the output string.
            StringBuilder builder = new StringBuilder();
            char c;
            for (int i = 0; i < input.Length; i++)
            {
                c = input[i];
                if ((c > 96 && c < 123) || // It's a lowercase letter.
                    (c > 47 && c < 58) || // It's a digit.
                    (c > 64 && c < 91))    // It's a capital letter.
                {
                    builder.Append(c);
                }
            }

            return builder.ToString();
        }
    }
}