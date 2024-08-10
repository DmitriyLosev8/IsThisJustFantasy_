using Assets.Scripts.GameLogic.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Weapons.Bows
{
    internal class Mark : MonoBehaviour
    {
        private float _offset = 2.5f;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void MarkEnemy(IDamageable enemy)
        {
            gameObject.SetActive(true);

            transform.position = enemy.Transform.position + Vector3.up * _offset;

            Vector3 worldPosition = transform.position + Camera.main.transform.rotation * -Vector3.back;
            Vector3 worldUp = Camera.main.transform.rotation * Vector3.up;

            transform.LookAt(worldPosition, worldUp);
        }

        public void UnMarkEnemy()
        {
            gameObject.SetActive(false);
        }
    }
}