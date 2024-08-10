using System;
using System.Collections.Generic;
using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.PlayerComponents
{
    internal class Player : MonoBehaviour
    {
        [SerializeField] private List<PlayerComponent> _components;
        [SerializeField] private PlayerData[] _levels;

        private int _currentLevel = 1;
        private int _maxLevel = 6;
        private PlayerWallet _wallet;

        public event Action<int> LevelChanged;

        public int CurrentLevel => _currentLevel;

        private void Awake()
        {
            _wallet = new PlayerWallet();
            PlayerSFX sfx = GetComponentInChildren<PlayerSFX>();

            if (PlayerPrefs.GetInt(PlayerConfigs.PlayerLevel) > 0)
            {
                _currentLevel = PlayerPrefs.GetInt(PlayerConfigs.PlayerLevel);
            }

#if !UNITY_WEBGL && UNITY_EDITOR
            _currentLevel = 0;
#endif

            for (int i = 0; i < _levels.Length; i++)
            {
                if (i == _currentLevel - 1)
                {
                    foreach (PlayerComponent component in _components)
                    {
                        component.Init(_levels[i], sfx);
                    }

                    break;
                }
            }
        }

        public void LevelUp()
        {
            if (_currentLevel < _maxLevel)
            {
                _currentLevel++;
                PlayerPrefs.SetInt(PlayerConfigs.PlayerLevel, _currentLevel);
                _wallet.Reset();
                LevelChanged?.Invoke(_currentLevel);
            }
        }
    }
}