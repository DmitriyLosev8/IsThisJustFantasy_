using System;
using Assets.Scripts.GameLogic.Interfaces;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Utilities
{
    internal class ClosestTargetFinder
    {
        private float _radius;
        private LayerMask _layerMask;
        private Collider[] _hitColliders;

        public ClosestTargetFinder(float radius, LayerMask layerMask)
        {
            _radius = radius;
            _layerMask = layerMask;
        }

        public bool TryFindTarget(Vector3 currentPosition, out IDamageable target)
        {
            _hitColliders = Physics.OverlapSphere(currentPosition, _radius, _layerMask);

            if (_hitColliders.Length > 0)
            {
                Array.Sort(_hitColliders, (Collider x, Collider y)
                    => Vector3.Distance(currentPosition, x.transform.position)
                    .CompareTo(Vector3.Distance(currentPosition, y.transform.position)));

                foreach (var hit in _hitColliders)
                {
                    if (hit.TryGetComponent<IDamageable>(out IDamageable enemy) && enemy.Health > 0)
                    {
                        target = enemy;

                        return true;
                    }
                }
            }

            target = null;

            return false;
        }
    }
}