using Assets.Scripts.PlayerComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _coins;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private SpriteChanger _changer;

        private PlayerHealth _health;
        private PlayerWallet _wallet;
        private PlayerAttacker _attacker;
        private int _playerLevel;

        private void OnDisable()
        {
            _attacker.WeaponChanged -= _changer.ChangeSprite;
            _health.ValueChanged -= OnHealthChanged;
            _wallet.CoinsChanged -= OnCoinsChanged;
        }

        public void SignToPlayerValuesChanges(Player player)
        {
            _wallet = new PlayerWallet();
            _health = player.GetComponent<PlayerHealth>();
            _attacker = player.GetComponent<PlayerAttacker>();
            _slider.maxValue = _health.Health;
            _slider.value = _health.Health;

            SetNumberOfLevel(player.CurrentLevel);

            _coins.text = _wallet.Coins.ToString();

            _attacker.WeaponChanged += _changer.ChangeSprite;
            _health.ValueChanged += OnHealthChanged;
            _wallet.CoinsChanged += OnCoinsChanged;
        }

        public void SetNumberOfLevel(int newPlayerLevel)
        {
            _playerLevel = newPlayerLevel;
            _level.text = _playerLevel.ToString();
        }

        private void OnHealthChanged(float health)
        {
            _slider.value = health;
        }

        private void OnCoinsChanged(int coins)
        {
            Debug.Log(coins);
            _coins.text = coins.ToString();
        }
    }
}