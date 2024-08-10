using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.YandexSDK
{
    internal class InterstitialAdTimer : MonoBehaviour
    {
        private InterstitialAdShower _interstitialAd;
        private WaitForSeconds _cooldown;
        private bool _isOnCooldown;
        private float _timeToShow = 60.5f;

        public event Action<bool> CooldownStarted;

        public event Action<bool> BecomeAvailable;

        public bool IsOnCooldown => _isOnCooldown;

        private void Start()
        {
            _cooldown = new WaitForSeconds(_timeToShow);
        }

        public void Init(InterstitialAdShower interstitialAd)
        {
            _interstitialAd = interstitialAd;
        }

        public void Start—ountDown()
        {
            _isOnCooldown = true;
            CooldownStarted?.Invoke(_isOnCooldown);
            _interstitialAd.Show();
            StartCoroutine(—ountDown());
        }

        private IEnumerator —ountDown()
        {
            yield return _cooldown;

            _isOnCooldown = false;
            BecomeAvailable?.Invoke(_isOnCooldown);
        }
    }
}