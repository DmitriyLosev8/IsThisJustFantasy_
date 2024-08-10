using Assets.Scripts.GameLogic.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    internal class Sword : Weapon
    {
        private RaycastHit[] _hitColliders;
        private float _maxDistance = 1f;
        private int _sizeScale = 5500;

        public override void Attack()
        {
            base.Attack();

            _hitColliders = Physics.BoxCastAll(
                transform.position,
                transform.localScale / _sizeScale,
                transform.forward,
                Quaternion.identity,
                _maxDistance,
                EnemyLayerMask);

            if (_hitColliders.Length > 0)
            {
                foreach (var hit in _hitColliders)
                {
                    if (hit.transform.gameObject.TryGetComponent(out IDamageable enemy) && enemy.Health > 0)
                    {
                        enemy.TakeDamage(Damage);
                    }
                }
            }
        }
    }
}