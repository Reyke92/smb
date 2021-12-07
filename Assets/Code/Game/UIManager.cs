using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SMB.Game
{
    internal class UIManager : MonoBehaviour
    {
        internal static UIManager Instance { get; private set; }

        internal EventHandler PauseMenuClosed;
        internal EventHandler PauseMenuOpened;

        [SerializeField]
        private Text _CoinsText;

        [SerializeField]
        private Text _PointsText;

        [SerializeField]
        private Text _TimeLeftText;

        private bool _IsPauseMenuOpen;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Start()
        {
            GameResources.Instance.CoinsChanged += _HandleCoinsChanged;
            GameResources.Instance.LivesChanged += _HandleLivesChanged;
            GameResources.Instance.PointsChanged += _HandlePointsChanged;
            GameResources.Instance.TimeLeftChanged += _HandleTimeLeftChanged;
        }

        internal void ShowPauseMenu(bool shouldShow)
        {

        }

        internal void TogglePauseMenu()
        {

        }

        private void _OnPauseMenuClosed()
        {
            if (PauseMenuClosed != null)
            {
                PauseMenuClosed(this, EventArgs.Empty);
            }
        }

        private void _OnPauseMenuOpened()
        {
            if (PauseMenuOpened != null)
            {
                PauseMenuOpened(this, EventArgs.Empty);
            }
        }

        private void _HandleCoinsChanged(object sender, ValueChangedEventArgs e)
        {
            _CoinsText.text = "x" + e.Value.ToString("00");
        }

        private void _HandleLivesChanged(object sender, ValueChangedEventArgs e)
        {
            // If player had no lives left, we don't need to update the UI.
            if (e.Value == -1) return;
        }

        private void _HandlePointsChanged(object sender, ValueChangedEventArgs e)
        {
            _PointsText.text = e.Value.ToString("000000");
        }

        private void _HandleTimeLeftChanged(object sender, ValueChangedEventArgs e)
        {
            _TimeLeftText.text = e.Value.ToString();
        }
    }
}