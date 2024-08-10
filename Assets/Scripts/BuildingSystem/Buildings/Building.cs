using System;
using Assets.Scripts.GameLogic.Interfaces;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]

    internal abstract class Building : MonoBehaviour, IDamageable
    {
        [SerializeField] private ColliderPanelEventer _eventer;
        [SerializeField] private ParticleSystem _particleOfDestroy;
        [SerializeField] private float _strength;
        [SerializeField] private float _valueOfScaleOfParticleOfDestroy;
        [SerializeField] private Transform _spotOfDestroyEffects;

        private AudioSource _audiosourse;
        private Vector3 _scaleOfParticleOfDestroy;
        private float _startStrength;

        public event Action Destroyed;

        public event Action<ColliderPanelEventer> BuildWithEventorWasMade;

        public Transform Transform => transform;

        public ColliderPanelEventer Eventer => _eventer;

        public float Health => _strength;

        private void Awake()
        {
            if (GetComponent<AudioSource>() != null)
            {
                _audiosourse = GetComponent<AudioSource>();
            }
        }

        private void OnEnable()
        {
            if (_audiosourse.clip != null)
            {
                _audiosourse.Play();
            }

            SubscribeToEventer();
        }

        private void Start()
        {
            _startStrength = _strength;
            SetScaleOfParticleOfDestroy();
        }

        public void TakeDamage(float damage)
        {
            if (_strength > 0 && damage > 0)
            {
                _strength -= damage;

                if (_strength <= 0)
                {
                    Destroy();
                }
            }
        }

        public void AnnounceOfCreation()
        {
            if (Eventer != null)
            {
                BuildWithEventorWasMade?.Invoke(Eventer);
            }
        }

        public abstract void SubscribeToEventer();

        protected void Destroy()
        {
            Instantiate(_particleOfDestroy, _spotOfDestroyEffects.position, Quaternion.identity);
            Destroyed?.Invoke();
            gameObject.SetActive(false);
            RefreshStrength();
        }

        private void SetScaleOfParticleOfDestroy()
        {
            _scaleOfParticleOfDestroy = new Vector3(
                _valueOfScaleOfParticleOfDestroy,
                _valueOfScaleOfParticleOfDestroy,
                _valueOfScaleOfParticleOfDestroy);

            _particleOfDestroy.transform.localScale = _scaleOfParticleOfDestroy;
        }

        private void RefreshStrength()
        {
            _strength = _startStrength;
        }
    }
}