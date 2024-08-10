using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.Props.Chest;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class ResoorceBuilding : Building
    {
        [SerializeField] private Chest _prefabOfChest;

        private int _currentIndexOfChestSpawnPoint;
        private int _firstChestSpawnPoint = 0;
        private List<ChestSpawnPoint> _currentSpawnPoints;

        private PlayerWallet _wallet = new PlayerWallet();

        private void OnDisable()
        {
            Eventer.FirstButtonClicked -= OnPrimaryButtonClicked;
            Eventer.SecondButtonClicked -= OnPrimaryButtonClicked;
        }

        public void SetChestsSpawnPoints(ChestSpawnerPointsContainer chestSpawnPoints)
        {
            _currentSpawnPoints = chestSpawnPoints.SpawnPoints;
        }

        public override void SubscribeToEventer()
        {
            Eventer.FirstButtonClicked += OnPrimaryButtonClicked;
            Eventer.SecondButtonClicked += OnPrimaryButtonClicked;
        }

        private void OnPrimaryButtonClicked(Player player, int costToBuy, int buttonIndex)
        {
            if (_currentSpawnPoints.Count != 0)
            {
                if (buttonIndex == UiHash.CoinsButtonIndex)
                {
                    if (_wallet.Coins >= costToBuy)
                    {
                        Spawn();
                        _wallet.SpendCoins(costToBuy);
                    }
                }

                if (buttonIndex == UiHash.AdButtonIndex)
                {
                    Spawn();
                    _currentSpawnPoints.RemoveAt(_currentIndexOfChestSpawnPoint);
                }
            }
        }

        private void Spawn()
        {
            int lastChestSpawnPoint = _currentSpawnPoints.Count;
            _currentIndexOfChestSpawnPoint = Random.Range(
                _firstChestSpawnPoint,
                lastChestSpawnPoint);

            Chest chestToSpawn = Instantiate(
                _prefabOfChest,
                _currentSpawnPoints[_currentIndexOfChestSpawnPoint].transform);

            chestToSpawn.SetCountOfCoins(_currentSpawnPoints[_currentIndexOfChestSpawnPoint].CoinsOfChest);
            _currentSpawnPoints.RemoveAt(_currentIndexOfChestSpawnPoint);
        }
    }
}