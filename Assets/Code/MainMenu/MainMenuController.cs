using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using SMB.Api;
using UnityEngine.SceneManagement;

namespace SMB.MainMenu
{
    internal class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _SelectGamePanel;

        [SerializeField]
        private Button _NewGameButton;

        [SerializeField]
        private GameObject _SelectCharacterPanel;

        [SerializeField]
        private Button _SelectCharacterBackButton;

        [SerializeField]
        private Button _SelectMarioButton;

        [SerializeField]
        private Button _SelectLuigiButton;

        private void Start()
        {
            // Add UI event listeners.
            _NewGameButton.onClick.AddListener(_HandleNewGameButtonClicked);
            _SelectCharacterBackButton.onClick.AddListener(_HandleSelectCharacterBackButtonClicked);
            _SelectMarioButton.onClick.AddListener(_HandleSelectMarioButtonClicked);
            _SelectLuigiButton.onClick.AddListener(_HandleSelectLuigiButtonClicked);
        }

        private void _HandleNewGameButtonClicked()
        {
            _SelectGamePanel.SetActive(false);
            _SelectCharacterPanel.SetActive(true);
        }

        private void _HandleSelectCharacterBackButtonClicked()
        {
            _SelectGamePanel.SetActive(true);
            _SelectCharacterPanel.SetActive(false);
        }

        private void _HandleSelectMarioButtonClicked()
        {
            SessionManager.GameSave = GameSave.CreateNew(PlayerType.Mario);
            SceneManager.LoadScene("1-1", LoadSceneMode.Single);
        }

        private void _HandleSelectLuigiButtonClicked()
        {
            SessionManager.GameSave = GameSave.CreateNew(PlayerType.Luigi);
            SceneManager.LoadScene("1-1", LoadSceneMode.Single);
        }
    }
}