using System;
using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.UI.Tutorial;
using Assets.Scripts.YandexSDK;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class ColliderPanelEventer : MonoBehaviour
    {
        [SerializeField] private int _costToBuy;
        [SerializeField] private GameObject _panelToShow;
        [SerializeField] private Button _firstButton;
        [SerializeField] private Button _adButton;
        [SerializeField] private Button _extraButton;
        [SerializeField] private TutorialPanel _tutorial;
        [SerializeField] private LeanToken _cost;
        [SerializeField] private InterstitialAdPopup _popupPanel;

        private Player _player;
        private bool _isActive;
        private float _changeScaleSpeed = 0.15f;
        private int _panelMoveXValue = 228;
        private InterstitialAdTimer _timer;
        private bool _isAdButtonOnCooldown = false;

        public event Action<Player, int, int> FirstButtonClicked;

        public event Action<Player, int, int> SecondButtonClicked;

        public event Action ExtraButtonClicked;

        public Button AdButton => _adButton;

        private void OnEnable()
        {
            _firstButton.onClick.AddListener(OnFirsttButtonClicked);

            if (_adButton != null)
            {
                _adButton.onClick.AddListener(OnAdButtonClicked);
            }

            if (_extraButton != null)
            {
                _extraButton.onClick.AddListener(OnExtraButtonClicked);
            }
        }

        private void Start()
        {
            _cost.SetValue(_costToBuy);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                _isActive = true;
                Open();
                _player = player;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                Close();
            }
        }

        private void OnDisable()
        {
            _firstButton.onClick.RemoveListener(OnFirsttButtonClicked);

            if (gameObject.activeSelf && _timer != null)
            {
                _timer.CooldownStarted -= OnTimerStateChanged;
                _timer.BecomeAvailable -= OnTimerStateChanged;
            }

            if (_adButton != null)
            {
                _adButton.onClick.RemoveListener(OnAdButtonClicked);
            }

            if (_extraButton != null)
            {
                _extraButton.onClick.RemoveListener(OnExtraButtonClicked);
            }

            Close();
        }

        public void TakeTimer(InterstitialAdTimer timer)
        {
            _timer = timer;
            _isAdButtonOnCooldown = _timer.IsOnCooldown;
            _timer.CooldownStarted += OnTimerStateChanged;
            _timer.BecomeAvailable += OnTimerStateChanged;
        }

        private void OnFirsttButtonClicked()
        {
            FirstButtonClicked?.Invoke(_player, _costToBuy, UiHash.CoinsButtonIndex);
        }

        private void OnAdButtonClicked()
        {
            if (_isAdButtonOnCooldown == false)
            {
                SecondButtonClicked?.Invoke(_player, _costToBuy, UiHash.AdButtonIndex);
                _timer.Start—ountDown();
            }
            else
            {
                StartCoroutine(_popupPanel.Show());
            }
        }

        private void OnExtraButtonClicked()
        {
            ExtraButtonClicked?.Invoke();
            Close();
        }

        private void OnTimerStateChanged(bool isOnCooldown)
        {
            _isAdButtonOnCooldown = isOnCooldown;
        }

        private void Open()
        {
            ChangeActiveStatus();
            _panelToShow.GetComponent<RectTransform>().LeanSetLocalPosX(
                transform.position.x + _panelMoveXValue);
            LeanTween.moveX(
                _panelToShow.GetComponent<RectTransform>(),
                -_panelMoveXValue,
                _changeScaleSpeed);

            if (PlayerPrefs.GetInt(PlayerConfigs.HasPassedTutorial) == 0)
            {
                _tutorial.Open();
            }
        }

        private void Close()
        {
            _isActive = false;
            LeanTween.moveX(
                _panelToShow.GetComponent<RectTransform>(),
                _panelMoveXValue,
                _changeScaleSpeed).setOnComplete(ChangeActiveStatus);

            if (PlayerPrefs.GetInt(PlayerConfigs.HasPassedTutorial) == 0)
            {
                _tutorial.OnPanelClosed();
            }
        }

        private void ChangeActiveStatus()
        {
            _panelToShow.gameObject.SetActive(_isActive);
        }
    }
}