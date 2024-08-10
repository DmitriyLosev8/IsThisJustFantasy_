using Agava.WebUtility;
using Assets.Scripts.UI;
using Assets.Scripts.YandexSDK;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    internal class BackgraoundPauser : MonoBehaviour
    {
        private Pauser _pauser;
        private PausePanel _pausePanel;
        private VideoAdShower _videoAd;
        private InterstitialAdShower _interstitialAd;
        private bool _isGameOnPause;

        private void OnDisable()
        {
            Application.focusChanged -= OnInBackgroundChangeApp;
        }

        public void Init(Pauser pauser, PausePanel pausePanel, VideoAdShower videoAd, InterstitialAdShower interstitialAd)
        {
            _pauser = pauser;
            _pausePanel = pausePanel;
            _videoAd = videoAd;
            _interstitialAd = interstitialAd;

            Application.focusChanged += OnInBackgroundChangeApp;
        }

        private void CheckCapableToPause()
        {
            if (_pausePanel.IsPaused || _videoAd.IsPaused || _interstitialAd.IsPaused)
            {
                _isGameOnPause = true;
            }
            else
            {
                _isGameOnPause = false;
            }
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            CheckCapableToPause();

            if (!_isGameOnPause)
            {
                if (!inApp)
                {
                    _pauser.Pause();
                }
                else
                {
                    _pauser.Resume();
                }
            }
        }
    }
}