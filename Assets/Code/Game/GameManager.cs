using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SMB.Game
{
    internal class GameManager : MonoBehaviour
    {
        // To try to match the original version (which used ticks, and not actual measurements of time).
        private const float _TIME_SPEEDUP_MULTIPLIER = 2.32327672322f;

        internal static GameManager Instance { get; private set; }

        private float _CurrentTimeLeft;
        private float _CurrentTimeLeftUpdateGoal;

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
            #if UNITY_EDITOR
            GameResources.Instance.Lives = 3;
            GameResources.Instance.Coins = 0;
            #endif
            #if !UNITY_EDITOR
            GameResources.Instance.Lives = SessionManager.GameSave.Lives;
            GameResources.Instance.Coins = SessionManager.GameSave.Coins;
            #endif
            GameResources.Instance.Points = 0; // This is not the total score, but just the score for the stage.
            GameResources.Instance.LevelNum = 1;
            GameResources.Instance.WorldNum = 1;

            _CurrentTimeLeft = 400f;
            _CurrentTimeLeftUpdateGoal = _CurrentTimeLeft - 1f;
            GameResources.Instance.TimeLeft = (int)_CurrentTimeLeft;

            GameResources.Instance.LivesChanged += _HandleLivesChanged;
        }

        internal void ExitToMainMenu()
        {
            SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single);
        }

        internal void GoToStage(int world, int level)
        {
            SceneManager.LoadSceneAsync($"{world}-{level}", LoadSceneMode.Single);
        }

        internal void GoToNextStage()
        {
            int world = GameResources.Instance.WorldNum + (GameResources.Instance.LevelNum / 4);
            int level = (GameResources.Instance.LevelNum % 4) + 1;
            GoToStage(world, level);
        }

        private void Update()
        {
            _CurrentTimeLeft -= Time.deltaTime * _TIME_SPEEDUP_MULTIPLIER;
            if (_CurrentTimeLeft < _CurrentTimeLeftUpdateGoal)
            {
                _CurrentTimeLeftUpdateGoal = Mathf.Floor(_CurrentTimeLeft);
                GameResources.Instance.TimeLeft = Mathf.FloorToInt(_CurrentTimeLeft);
            }
        }

        private void _HandleLivesChanged(object sender, ValueChangedEventArgs e)
        {
            GoToStage(1, 1);
        }

        private void _HandlePauseMenuClosed()
        {

        }

        private void _HandlePauseMenuOpened()
        {

        }

        private void _HandlePauseMenuRequested()
        {

        }
    }
}