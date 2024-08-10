using System;
using Assets.Scripts.PlayerComponents;
using UnityEngine;

namespace Assets.Scripts.UI.Tutorial
{
    internal class TutorialArrow : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private Transform _player;
        private Vector3 _positionOffset = new Vector3(0f, 3f, 0);
        private Quaternion _rotationOffset = Quaternion.Euler(0, 90, 0);
        private Quaternion _rotationAtChest = Quaternion.Euler(0, 0, 90);
        private float _maxDistance = 5f;

        public event Action Collected;

        private void LateUpdate()
        {
            Vector3 directionToChest = new Vector3(
                _target.position.x - transform.position.x,
                0,
                _target.position.z - transform.position.z);

            if (Vector3.Distance(_player.position, _target.position) > _maxDistance)
            {
                Quaternion rotationToChest = Quaternion.LookRotation(directionToChest);

                transform.position = _player.position + _positionOffset;
                transform.rotation = _rotationOffset * rotationToChest;
            }
            else
            {
                transform.position = _target.position + _positionOffset;
                transform.rotation = _rotationAtChest;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                gameObject.SetActive(false);
                Collected?.Invoke();
            }
        }

        public void Init(Transform player)
        {
            _player = player;
        }
    }
}