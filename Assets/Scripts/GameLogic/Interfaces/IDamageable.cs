using UnityEngine;

namespace Assets.Scripts.GameLogic.Interfaces
{
    internal interface IDamageable
    {
        public float Health { get; }

        public Transform Transform { get; }

        void TakeDamage(float damage);
    }
}