using System.Collections;
using Assets.Scripts.GameLogic.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Weapons.Bows
{
    [RequireComponent(typeof(AudioSource))]
    internal class Arrow : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _hitEffect;
        [SerializeField] private float _speed;

        private LayerMask _layerMask;
        private AudioSource _audiosourse;

        private Vector3 _correctPosition;
        private Vector3 _offset = Vector3.up * 1.5f;

        private float _damage;

        private Coroutine _flying;

        private void Awake()
        {
            _audiosourse = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            int mask = 1 << other.gameObject.layer;

            if (other.gameObject.TryGetComponent(out IDamageable target) && mask == _layerMask)
            {
                target.TakeDamage(_damage);
                ParticleSystem hitEffect = Instantiate(_hitEffect, transform.position, Quaternion.identity);
                Destroy(hitEffect.gameObject, 1f);

                if (_flying != null)
                {
                    StopCoroutine(_flying);
                }

                gameObject.SetActive(false);
            }
        }

        public void Fly(Transform target)
        {
            if (_flying != null)
            {
                StopCoroutine(_flying);
            }

            _flying = StartCoroutine(Flying(target));
            _audiosourse.Play();
        }

        public void Init(float damage, LayerMask targetMask)
        {
            _damage = damage;
            _layerMask = targetMask;
        }

        private IEnumerator Flying(Transform target)
        {
            while (target != null && Vector3.Distance(transform.position, target.position) > 0.1f &&
                target.transform.gameObject.activeSelf)
            {
                _correctPosition = target.position + _offset;

                Vector3 relativePosition = _correctPosition - transform.position;

                Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.up);

                Vector3 position = Vector3.MoveTowards(
                    transform.position,
                    _correctPosition,
                    _speed * Time.fixedDeltaTime);

                transform.SetPositionAndRotation(position, rotation);

                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}