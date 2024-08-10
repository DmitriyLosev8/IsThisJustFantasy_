using Assets.Scripts.Audio;
using Assets.Scripts.GameLogic;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.YandexSDK;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    internal class UiService : MonoBehaviour
    {
        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private PausePanel _pausePanel;
        [SerializeField] private SoundToggler _soundToggler;
        [SerializeField] private GameObject _waves;
        [SerializeField] private TMP_Text _wavesNumber;
        [SerializeField] private ScorePanel _nextLevelPanel;
        [SerializeField] private ScorePanel _endGamePanel;
        [SerializeField] private ScorePanel _winGamePanel;

        private SceneLoader _sceneLoader;
        private AudioMixer _audioMixer;
        private Player _player;
        private InterstitialAdShower _interstitialAd;

        public ScorePanel NextLevelPanel => _nextLevelPanel;

        public ScorePanel EndGamePanel => _endGamePanel;

        public ScorePanel WinGamePanel => _winGamePanel;

        public PausePanel PausePanel => _pausePanel;

        private void OnDisable()
        {
            _audioMixer.VolumeValueChanged -= _soundToggler.SetCurrentStatus;
            _player.LevelChanged -= _playerUI.SetNumberOfLevel;

            UnSignToPanels();
        }

        public void Init(
            Player player,
            SceneLoader loader,
            AudioMixer mixer,
            Pauser pauser,
            InterstitialAdShower adShower)
        {
            _sceneLoader = loader;
            _player = player;
            _audioMixer = mixer;
            _interstitialAd = adShower;

            _player.LevelChanged += _playerUI.SetNumberOfLevel;
            _playerUI.SignToPlayerValuesChanges(player);
            _pausePanel.Init(pauser);

            _audioMixer.SignSoundValuesChanges(_soundToggler);
            _audioMixer.VolumeValueChanged += _soundToggler.SetCurrentStatus;

            _sceneLoader.SignToPausePanelEvents(_pausePanel);

            SignToPanels();
        }

        public void OnWaveStarted(int amount)
        {
            _wavesNumber.text = amount.ToString();
            _waves.gameObject.SetActive(true);
        }

        public void OnWaveSpawnAmountChanged(int amount)
        {
            if (amount > 0)
            {
                _wavesNumber.text = amount.ToString();
            }
            else
            {
                _waves.gameObject.SetActive(false);
            }
        }

        private void SignToPanels()
        {
            _nextLevelPanel.ContinueButtonPressed += _interstitialAd.Show;
            _endGamePanel.ContinueButtonPressed += _interstitialAd.Show;
            _endGamePanel.BackButtonPressed += _interstitialAd.Show;
            _winGamePanel.BackButtonPressed += _interstitialAd.Show;
        }

        private void UnSignToPanels()
        {
            _nextLevelPanel.ContinueButtonPressed -= _interstitialAd.Show;
            _endGamePanel.ContinueButtonPressed -= _interstitialAd.Show;
            _endGamePanel.BackButtonPressed -= _interstitialAd.Show;
            _winGamePanel.BackButtonPressed -= _interstitialAd.Show;
        }
    }
}