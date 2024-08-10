using Assets.Scripts.GameLogic.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class UnitHealthbar : MonoBehaviour
    {
        [SerializeField] private Slider _health;
        [SerializeField] private GameObject _unit;

        private IHealthDisplayable _displayable;

        private void OnEnable()
        {
            if (_unit.TryGetComponent(out IHealthDisplayable unit))
            {
                _displayable = unit;

                _displayable.HealthValueChanged += OnHealthChanged;
            }
        }

        private void Update()
        {
            transform.LookAt(
                transform.position + Camera.main.transform.rotation * Vector3.back,
                Camera.main.transform.rotation * Vector3.up);
        }

        private void OnDisable()
        {
            _displayable.HealthValueChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(float health)
        {
            _health.value = health;
        }
    }
}