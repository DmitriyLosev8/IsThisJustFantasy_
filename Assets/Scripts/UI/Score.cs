using Agava.YandexGames;
using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic;
using Assets.Scripts.PlayerComponents;
using UnityEngine;

namespace Assets.Scripts.UI
{
    internal class Score : MonoBehaviour
    {
        private ScorePanel _endGamePanel;

        private int _totalScore;
        private int _levelScore;
        private Pauser _pauser;
        private PlayerWallet _wallet;
        private PlayerHealth _health;
        private SceneLoader _sceneLoader;
        private int _scoreMultiplier = 2;

        private void OnDisable()
        {
            _endGamePanel.ContinueButtonPressed -= OnRestartButtonPressed;
            _endGamePanel.ContinueButtonPressed -= OnRestartButtonPressed;

            _health.Diead -= OpenEndGamePanel;
        }

        public void Init(Player player, Pauser pauser, SceneLoader sceneLoader, ScorePanel endGamePanel)
        {
            _endGamePanel = endGamePanel;
            _endGamePanel.BackButtonPressed += OnMenuButtonPressed;
            _endGamePanel.ContinueButtonPressed += OnRestartButtonPressed;

            _wallet = new PlayerWallet();
            _health = player.GetComponent<PlayerHealth>();
            _pauser = pauser;
            _sceneLoader = sceneLoader;

            _health.Diead += OpenEndGamePanel;

            LoadScore();
        }

        public void UpdateTotalScore()
        {
            _totalScore += _levelScore;

            SaveScore(_totalScore);
            SaveLeaderboardScore(_totalScore);
        }

        public int GetLevelScore()
        {
            _levelScore = _wallet.Coins * _scoreMultiplier;

            return _levelScore;
        }

        public void OpenEndGamePanel()
        {
            _endGamePanel.gameObject.SetActive(true);
            _pauser.Pause();
        }

        private void LoadScore()
        {
            if (PlayerPrefs.HasKey(PlayerConfigs.Score))
            {
                _totalScore = PlayerPrefs.GetInt(PlayerConfigs.Score);
            }
        }

        private void SaveScore(int value)
        {
            PlayerPrefs.SetInt(PlayerConfigs.Score, value);
            PlayerPrefs.Save();
        }

        private void SaveLeaderboardScore(int value)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized)
            {
                Agava.YandexGames.Leaderboard.GetPlayerEntry(PlayerConfigs.Leaderboard, response =>
                {
                    Agava.YandexGames.Leaderboard.SetScore(PlayerConfigs.Leaderboard, value);
                });
            }
#endif
        }

        private void OnRestartButtonPressed()
        {
            _endGamePanel.gameObject.SetActive(false);
            _sceneLoader.RestartCurrentScene();
            _pauser.Resume();
        }

        private void OnMenuButtonPressed()
        {
            _endGamePanel.gameObject.SetActive(false);
            _sceneLoader.LoadMenuScene();
            _pauser.Resume();
        }
    }
}