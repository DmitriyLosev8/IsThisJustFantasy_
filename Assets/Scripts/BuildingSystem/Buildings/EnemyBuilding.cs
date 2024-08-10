using System;
using Assets.Scripts.Constants;
using Assets.Scripts.EnemyComponents.Factory;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.PlayerUnits;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class EnemyBuilding : MonoBehaviour
    {
        [SerializeField] private ColliderPanelEventer _eventer;

        private EnemyFactory _enemyFactory;
        private PlayerWallet _playerWallet;

        public event Action<ColliderPanelEventer> BuildWithEventorWasMade;

        public Button AdButton => _eventer.AdButton;
        public ColliderPanelEventer EventerToSend => _eventer;

        private void Awake()
        {
            AnnounceOfCreation();
            _playerWallet = new PlayerWallet();
        }

        private void OnEnable()
        {
            _enemyFactory = GetComponentInChildren<EnemyFactory>();

            if (_enemyFactory == null)
                throw new NullReferenceException("_enemyFactory is null");

            _enemyFactory.WaveStarted += OnWaveStart;
            _enemyFactory.WaveEnded += OnWaveEnd;
            _eventer.FirstButtonClicked += OnPrimaryButtonClicked;
            _eventer.SecondButtonClicked += OnPrimaryButtonClicked;
            _eventer.ExtraButtonClicked += OnExtraButtonClicked;
        }

        private void OnDisable()
        {
            _enemyFactory.WaveStarted -= OnWaveStart;
            _enemyFactory.WaveEnded -= OnWaveEnd;
            _eventer.FirstButtonClicked -= OnPrimaryButtonClicked;
            _eventer.SecondButtonClicked -= OnPrimaryButtonClicked;
            _eventer.ExtraButtonClicked -= OnExtraButtonClicked;
        }

        public void AnnounceOfCreation()
        {
            if (_eventer != null)
            {
                BuildWithEventorWasMade?.Invoke(_eventer);
            }
        }

        private void OnPrimaryButtonClicked(Player player, int costToBuy, int buttonIndex)
        {
            if (buttonIndex == UiHash.CoinsButtonIndex && _playerWallet.Coins >= costToBuy)
            {
                _enemyFactory.IncreaseSpawnAmount();
                _playerWallet.SpendCoins(costToBuy);
            }

            if (buttonIndex == UiHash.AdButtonIndex)
            {
                _enemyFactory.DecreaceSpawnAmount();
            }
        }

        private void OnExtraButtonClicked()
        {
            _enemyFactory.StartWave();
        }

        private void OnWaveStart(int spawnAmount)
        {
            _eventer.gameObject.SetActive(false);
        }

        private void OnWaveEnd()
        {
            _eventer.gameObject.SetActive(true);
        }
    }
}