using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class PausePanel : MonoBehaviour
    {
        [SerializeField] private Button _restartSceneButton;
        [SerializeField] private Button _exitToMainMenuButton;
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _panel;

        private Pauser _pauser;

        public event Action MainMenuButtonClicked;

        public event Action RestartSceneButtonClicked;

        public bool IsPaused { get; private set; }

        private void OnEnable()
        {
            _openButton.onClick.AddListener(OnOpenButtonClicked);
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
            _exitToMainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            _restartSceneButton.onClick.AddListener(OnRestartSceneButtonClicked);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(OnOpenButtonClicked);
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
            _exitToMainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
            _restartSceneButton.onClick.RemoveListener(OnRestartSceneButtonClicked);
        }

        public void Init(Pauser pauser)
        {
            _pauser = pauser;
        }

        private void OnMainMenuButtonClicked()
        {
            _pauser.Resume();
            IsPaused = false;
            MainMenuButtonClicked?.Invoke();
        }

        private void OnRestartSceneButtonClicked()
        {
            RestartSceneButtonClicked?.Invoke();
            _pauser.Resume();
            IsPaused = false;
        }

        private void OnOpenButtonClicked()
        {
            _panel.SetActive(true);
            _pauser.Pause();
            IsPaused = true;
        }

        private void OnCloseButtonClicked()
        {
            _panel.SetActive(false);
            _pauser.Resume();
            IsPaused = false;
        }
    }
}