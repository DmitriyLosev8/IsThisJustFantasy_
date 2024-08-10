using System;
using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.PlayerUnits;
using Assets.Scripts.Units;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class Barracks : Building
    {
        private KnightFactory _unitsFactory;
        private PlayerWallet _playerWallet;

        private void OnDisable()
        {
            Eventer.FirstButtonClicked -= SpawnUnit;
            Eventer.SecondButtonClicked -= SpawnUnit;
        }

        public void Init(SelectedUnitsHandler handler)
        {
            _unitsFactory = GetComponentInChildren<KnightFactory>();

            if (_unitsFactory == null)
                throw new NullReferenceException("_unitsFactory is null");

            _playerWallet = new PlayerWallet();
            _unitsFactory.Init(handler);
        }

        public override void SubscribeToEventer()
        {
            Eventer.FirstButtonClicked += SpawnUnit;
            Eventer.SecondButtonClicked += SpawnUnit;
        }

        private void SpawnUnit(Player player, int costToBuy, int buttonIndex)
        {
            if (buttonIndex == UiHash.CoinsButtonIndex)
            {
                if (_playerWallet.Coins >= costToBuy)
                {
                    _unitsFactory.Spawn();
                    _playerWallet.SpendCoins(costToBuy);
                }
            }

            if (buttonIndex == UiHash.AdButtonIndex)
            {
                _unitsFactory.Spawn();
            }
        }
    }
}