using System;
using Assets.Scripts.PlayerComponents;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class MainBuilding : Building
    {
        private PlayerWallet _playerWallet = new PlayerWallet();

        private int _valueToHeal = 50;

        private void OnDisable()
        {
            Eventer.FirstButtonClicked -= HealPlayer;
        }

        public override void SubscribeToEventer()
        {
            Eventer.FirstButtonClicked += HealPlayer;
        }

        private void HealPlayer(Player player, int costToBuy, int buttonIndex)
        {
            PlayerHealth playerHealth = player.gameObject.GetComponent<PlayerHealth>() ??
                throw new NullReferenceException("playerHealth is null");

            if (_playerWallet.Coins >= costToBuy && playerHealth.Health < playerHealth.MaxHealth)
            {
                player.GetComponent<PlayerHealth>().Heal(_valueToHeal);
                _playerWallet.SpendCoins(costToBuy);
            }
        }
    }
}