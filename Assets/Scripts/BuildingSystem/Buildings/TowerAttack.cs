using Assets.Scripts.GameLogic.Interfaces;
using Assets.Scripts.GameLogic.Utilities;
using Assets.Scripts.Weapons.Bows;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem.Buildings
{
    internal class TowerAttack : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Arrow _arrowPrefab;
        [SerializeField] private float _delayOfShoot;
        [SerializeField] private float _damage;
        [SerializeField] private float _range;
        [SerializeField] private LayerMask _targetlayerMask;

        private ClosestTargetFinder _targetFinder;
        private ArrowsPool _poolOfArrows;
        private float _currentDelay;

        private void Awake()
        {
            _poolOfArrows = new ArrowsPool(_arrowPrefab, _damage, _targetlayerMask);
            _targetFinder = new ClosestTargetFinder(_range, _targetlayerMask);
        }

        private void Update()
        {
            if (_currentDelay >= _delayOfShoot &&
                _targetFinder.TryFindTarget(transform.position, out IDamageable target))
            {
                if (target.Health > 0)
                    Shoot(target);

                _currentDelay = 0;
            }

            _currentDelay += Time.deltaTime;
        }

        private void Shoot(IDamageable target)
        {
            Arrow arrow = _poolOfArrows.GetArrow();
            arrow.transform.position = _shootPoint.position;
            arrow.Fly(target.Transform);
        }
    }
}