using System;
using Assets.Scripts.GameLogic.Utilities;
using Assets.Scripts.PlayerComponents;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.BuildingSystem.System
{
    internal class BuilderButton : MonoBehaviour
    {
        [SerializeField] private Button _build;
        [SerializeField] private LeanToken _cost;
        [SerializeField] private LeanToken _building;

        private ButtonTranslator _translator = new ButtonTranslator();
        private Vector3 _closeValues = Vector3.zero;
        private Vector3 _openValues = new Vector3(1, 1, 1);
        private float _changeScaleSpeed = 0.1f;
        private bool _status;

        public event Action BuildButtonClicked;

        private void OnEnable()
        {
            _build.onClick.AddListener(OnBuildButtonClicked);
        }

        private void OnDisable()
        {
            _build.onClick.RemoveListener(OnBuildButtonClicked);
        }

        public void ToggleButton(int builPointIndex, int costToBuy, bool isPlayerIn)
        {
            _status = isPlayerIn;

            if (isPlayerIn)
            {
                Open();
                SetButtonText(builPointIndex, costToBuy);
                _build.gameObject.SetActive(isPlayerIn);
            }
            else
            {
                Close();
            }
        }

        private void SetButtonText(int builPointIndex, int costToBuy)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _building.SetValue(_translator.GetTranslation(builPointIndex));
            _cost.SetValue(costToBuy);
#endif
        }

        private void Close()
        {
            LeanTween.scale(_build.gameObject, _closeValues, _changeScaleSpeed).setOnComplete(ChangeStatus);
        }

        private void Open()
        {
            LeanTween.scale(_build.gameObject, _openValues, _changeScaleSpeed);
        }

        private void ChangeStatus()
        {
            _build.gameObject.SetActive(_status);
        }

        private void OnBuildButtonClicked()
        {
            BuildButtonClicked?.Invoke();
        }
    }
}