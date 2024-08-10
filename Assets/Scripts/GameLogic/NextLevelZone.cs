using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    internal class NextLevelZone : MonoBehaviour
    {
        private ScorePanel _nextLevelPanel;
        private ScorePanel _winPanel;
        private Score _score;
        private SceneLoader _sceneLoader;
        private Player _player;
        private Pauser _pauser;
        private bool _isLastLevelReached = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                OpenNextLevelPanel();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                _nextLevelPanel.gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            _sceneLoader.LastLevelReached -= OnLastLevelReached;
            UnSignToPanelsEvents();
        }

        public void Init(
            Score score,
            SceneLoader sceneLoader,
            Player player,
            Pauser pauser,
            ScorePanel nextLevelPanel,
            ScorePanel winPanel)
        {
            _pauser = pauser;
            _nextLevelPanel = nextLevelPanel;
            _winPanel = winPanel;
            _player = player;
            _score = score;
            _sceneLoader = sceneLoader;

            SignToPanelsEvents();
            _sceneLoader.LastLevelReached += OnLastLevelReached;
        }

        public void OnAllWavesDefeated()
        {
            gameObject.SetActive(true);
        }

        private void SignToPanelsEvents()
        {
            _nextLevelPanel.BackButtonPressed += OnBackButtonPressed;
            _nextLevelPanel.ContinueButtonPressed += OnContinueLevelButtonPressed;
            _winPanel.BackButtonPressed += OnMenuButtonPressed;
            _winPanel.ContinueButtonPressed += OnBackButtonPressed;
        }

        private void UnSignToPanelsEvents()
        {
            _nextLevelPanel.BackButtonPressed -= OnBackButtonPressed;
            _nextLevelPanel.ContinueButtonPressed -= OnContinueLevelButtonPressed;
            _winPanel.BackButtonPressed -= OnMenuButtonPressed;
            _winPanel.ContinueButtonPressed -= OnBackButtonPressed;
        }

        private void OpenNextLevelPanel()
        {
            if (_isLastLevelReached == false)
            {
                _nextLevelPanel.SetTextScore(_score.GetLevelScore().ToString());
                _nextLevelPanel.gameObject.SetActive(true);
                _pauser.Pause();
            }
            else
            {
                _winPanel.SetTextScore(_score.GetLevelScore().ToString());
                _winPanel.gameObject.SetActive(true);
                _pauser.Pause();
            }
        }

        private void OnContinueLevelButtonPressed()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerPrefs.SetInt(SceneNames.LastAvailableLevel, PlayerConfigs.LevelsCleared += 1);
#endif

            _player.LevelUp();
            _nextLevelPanel.gameObject.SetActive(false);
            _score.UpdateTotalScore();
            _pauser.Resume();
            _sceneLoader.LoadNextScene();
        }

        private void OnMenuButtonPressed()
        {
            _winPanel.gameObject.SetActive(false);
            _sceneLoader.LoadMenuScene();
            _pauser.Resume();
        }

        private void OnLastLevelReached(bool isLastLevelReached)
        {
            _isLastLevelReached = isLastLevelReached;
        }

        private void OnBackButtonPressed()
        {
            if (_isLastLevelReached == false)
            {
                _nextLevelPanel.gameObject.SetActive(false);
                _pauser.Resume();
            }
            else
            {
                _winPanel.gameObject.SetActive(false);
                _pauser.Resume();
            }
        }
    }
}