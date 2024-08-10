using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class ScorePanel : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private TMP_Text _textScore;

        public event Action BackButtonPressed;

        public event Action ContinueButtonPressed;

        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        }

        public void SetTextScore(string score)
        {
            _textScore.text = score;
        }

        private void OnBackButtonClicked()
        {
            BackButtonPressed?.Invoke();
        }

        private void OnContinueButtonClicked()
        {
            ContinueButtonPressed?.Invoke();
        }
    }
}