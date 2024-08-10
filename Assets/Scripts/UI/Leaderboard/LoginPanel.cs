using System;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Leaderboard
{
    internal class LoginPanel : Screen
    {
        [SerializeField] private Button _accept;
        [SerializeField] private Button _decline;

        public event Action Decline;

        public event Action Accept;

        private void OnEnable()
        {
            _accept.onClick.AddListener(OnAccept);
            _decline.onClick.AddListener(OnDecline);
        }

        private void OnDisable()
        {
            _accept.onClick.RemoveListener(OnAccept);
            _decline.onClick.RemoveListener(OnDecline);
        }

        private void OnAccept()
        {
            PlayerAccount.Authorize();

            OnPanelClosed();

            Accept?.Invoke();
        }

        private void OnDecline()
        {
            OnPanelClosed();

            Decline?.Invoke();
        }
    }
}