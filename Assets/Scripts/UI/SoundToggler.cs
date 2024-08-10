using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class SoundToggler : MonoBehaviour
    {
        [SerializeField] private Sprite _ckekMarkOn;
        [SerializeField] private Sprite _ckekMarkOff;
        [SerializeField] private Button _muteButton;

        private bool _isMuted;

        public event Action<bool> SoundValueChanged;

        private void OnEnable()
        {
            _muteButton.onClick.AddListener(OnMuteButtonPressed);
        }

        private void OnDisable()
        {
            _muteButton.onClick.RemoveListener(OnMuteButtonPressed);
        }

        public void SetCurrentStatus(bool isMuted)
        {
            _isMuted = isMuted;
            SetCurrentImage();
        }

        private void SetCurrentImage()
        {
            if (_isMuted)
            {
                _muteButton.image.sprite = _ckekMarkOff;
            }
            else
            {
                _muteButton.image.sprite = _ckekMarkOn;
            }
        }

        private void OnMuteButtonPressed()
        {
            _isMuted = !_isMuted;

            SoundValueChanged?.Invoke(_isMuted);
            SetCurrentImage();
        }
    }
}