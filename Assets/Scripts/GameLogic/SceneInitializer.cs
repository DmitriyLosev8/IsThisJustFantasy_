using Agava.WebUtility;
using Assets.Scripts.Audio;
using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.BuildingSystem.System;
using Assets.Scripts.CameraComponents;
using Assets.Scripts.EnemyComponents.Factory;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.PlayerInput;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Tutorial;
using Assets.Scripts.Units;
using Assets.Scripts.YandexSDK;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameLogic
{
    internal class SceneInitializer : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private MainBuilding _mainBuilding;
        [SerializeField] private TargetFollower _targetFollower;
        [SerializeField] private DesktopInput _desktopInput;
        [SerializeField] private MobileInput _mobileInput;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private UiService _globalUI;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private BuildingService _buildingSystem;
        [SerializeField] private NextLevelZone _nextLevelZone;
        [SerializeField] private InterstitialAdShower _interstitialAd;
        [SerializeField] private VideoAdShower _videoAd;
        [SerializeField] private Score _score;
        [SerializeField] private EnemyBuilding _enemyBuilding;
        [SerializeField] private BackgraoundPauser _backgroundPauser;
        [SerializeField] private InterstitialAdTimer _interstitialAdTimer;
        [SerializeField] private Tutorial _tutorial;

        private SelectedUnitsHandler _selectedUnitsHandler;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            _buildingSystem.EventerWithAdButtonWasMade += OnEventerWasMade;
            _enemyFactory.FinalWaveCleared += _nextLevelZone.OnAllWavesDefeated;
            _enemyBuilding.EventerToSend.TakeTimer(_interstitialAdTimer);
            _mainBuilding.BuildWithEventorWasMade += OnEventerWasMade;
            _mainBuilding.Destroyed += _score.OpenEndGamePanel;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            _selectedUnitsHandler.Dispose();
            _buildingSystem.EventerWithAdButtonWasMade -= OnEventerWasMade;
            _enemyFactory.FinalWaveCleared -= _nextLevelZone.OnAllWavesDefeated;
            _enemyFactory.WaveStarted -= _globalUI.OnWaveStarted;
            _enemyFactory.WaveSpawnAmountChanged -= _globalUI.OnWaveSpawnAmountChanged;
            _enemyBuilding.AdButton.onClick.RemoveListener(_interstitialAd.Show);
            _mainBuilding.BuildWithEventorWasMade -= OnEventerWasMade;
            _mainBuilding.Destroyed -= _score.OpenEndGamePanel;
        }

        private void OnEventerWasMade(ColliderPanelEventer currentEventer)
        {
            currentEventer.TakeTimer(_interstitialAdTimer);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Player player = InitializePlayer();

            MobileInput input = InitializeInput(player);

            Pauser pauser = new Pauser(_audioMixer, input);

            _buildingSystem.Init(_selectedUnitsHandler);

            InitializeUI(player, pauser);

            if (_tutorial != null)
            {
                _tutorial.Init(player.transform);
            }
        }

        private Player InitializePlayer()
        {
            Player player = Instantiate(_player, transform.position, Quaternion.identity);
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();

            _targetFollower.Init(player.transform);
            agent.Warp(transform.position);

            return player;
        }

        private MobileInput InitializeInput(Player player)
        {
            _selectedUnitsHandler = new SelectedUnitsHandler();

#if UNITY_WEBGL && !UNITY_EDITOR

            if (Device.IsMobile)
            {
                MobileInput input = Instantiate(_mobileInput, transform);
                input.Init(player, _selectedUnitsHandler);

                return input;
            }
            else
            {
                DesktopInput input = Instantiate(_desktopInput, transform);
                input.Init(player, _selectedUnitsHandler);

                return null;
            }

            throw new System.Exception("What is Device");
#endif

            DesktopInput input = Instantiate(_desktopInput, transform);
            input.Init(player, _selectedUnitsHandler);

            return null;
        }

        private void InitializeUI(Player player, Pauser pauser)
        {
            _interstitialAd.Init(pauser);
            _videoAd.Init(pauser);
            _interstitialAdTimer.Init(_interstitialAd);

            _globalUI.Init(player, _sceneLoader, _audioMixer, pauser, _interstitialAd);
            _score.Init(player, pauser, _sceneLoader, _globalUI.EndGamePanel);
            _nextLevelZone.Init(_score, _sceneLoader, player, pauser, _globalUI.NextLevelPanel, _globalUI.WinGamePanel);
            _backgroundPauser.Init(pauser, _globalUI.PausePanel, _videoAd, _interstitialAd);

            _enemyFactory.WaveStarted += _globalUI.OnWaveStarted;
            _enemyFactory.WaveSpawnAmountChanged += _globalUI.OnWaveSpawnAmountChanged;
        }
    }
}