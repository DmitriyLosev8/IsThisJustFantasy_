using System;
using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.PlayerComponents
{
    internal class PlayerWallet
    {
        private int _coins = 100;
        private int _newLevelCoinsAmount = 100;

        public event Action<int> CoinsChanged;

        public int Coins => _coins;

        public void SpendCoins(int amount)
        {
            _coins = PlayerPrefs.GetInt(PlayerConfigs.Coins);

            if (_coins >= amount)
            {
                _coins -= amount;
                SaveCoins();
            }
        }

        public void AddCoins(int amount)
        {
            _coins = PlayerPrefs.GetInt(PlayerConfigs.Coins);
            _coins += amount;
            SaveCoins();
        }

        public void Reset()
        {
            _coins = _newLevelCoinsAmount;
        }

        private void SaveCoins()
        {
            PlayerPrefs.SetInt(PlayerConfigs.Coins, _coins);
            CoinsChanged?.Invoke(_coins);
        }
    }
}