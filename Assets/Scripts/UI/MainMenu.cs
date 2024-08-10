using Agava.YandexGames;
using Assets.Scripts.UI.Leaderboard;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class MainMenu : MonoBehaviour
    {
        [SerializeField] private LeanLocalization _localization;
        [SerializeField] private LeaderboardScreen _leaderboardScreen;
        [SerializeField] private LevelsPanel _levelsPanel;
        [SerializeField] private Button _openLeaderboardScreenButton;
        [SerializeField] private Button _openLevelsPanelButton;
        [SerializeField] private Button _closeLevelsPanelButton;

        private void Awake()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            YandexGamesSdk.GameReady();

            switch (YandexGamesSdk.Environment.i18n.lang)
            {
                case "en":
                    _localization.SetCurrentLanguage("English");
                    break;

                case "ru":
                    _localization.SetCurrentLanguage("Russian");
                    break;

                case "tr":
                    _localization.SetCurrentLanguage("Turkish");
                    break;

                default:
                    _localization.SetCurrentLanguage("English");
                    break;
            }

            LeanLocalization.UpdateTranslations();
#endif
        }

        private void OnEnable()
        {
            _openLeaderboardScreenButton.onClick.AddListener(OpenLeaderboardScreen);
            _openLevelsPanelButton.onClick.AddListener(OpenLevelsPanel);
            _closeLevelsPanelButton.onClick.AddListener(CloseLevelsPanel);
        }

        private void OnDisable()
        {
            _openLeaderboardScreenButton.onClick.RemoveListener(OpenLeaderboardScreen);
            _openLevelsPanelButton.onClick.RemoveListener(OpenLevelsPanel);
            _closeLevelsPanelButton.onClick.RemoveListener(CloseLevelsPanel);
        }

        private void OpenLeaderboardScreen()
        {
            _leaderboardScreen.Open();
        }

        private void OpenLevelsPanel()
        {
            _levelsPanel.Init();
        }

        private void CloseLevelsPanel()
        {
            _levelsPanel.OnPanelClosed();
        }
    }
}