using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Leaderboard
{
    internal class LeaderboardScreen : Screen
    {
        [SerializeField] private LoginPanel _loginpanel;
        [SerializeField] private LeaderboardPanel _leaderboardpanel;
        [SerializeField] private Button _close;

        private void OnEnable()
        {
            _close.onClick.AddListener(OnPanelClosed);

            _leaderboardpanel.Closed += OnPanelClosed;
            _loginpanel.Accept += OnPanelClosed;
            _loginpanel.Decline += OnPanelClosed;
        }

        private void OnDisable()
        {
            _close.onClick.RemoveListener(OnPanelClosed);

            _leaderboardpanel.Closed -= OnPanelClosed;
            _loginpanel.Accept -= OnPanelClosed;
            _loginpanel.Decline -= OnPanelClosed;
        }

        public override void Open()
        {
            base.Open();

            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();

                _leaderboardpanel.Init();
            }
            else
            {
                _loginpanel.Open();
            }
        }
    }
}