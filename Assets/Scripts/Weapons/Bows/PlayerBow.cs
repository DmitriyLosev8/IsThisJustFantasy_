using System.Collections;
using Assets.Scripts.GameLogic.Interfaces;
using Assets.Scripts.GameLogic.Utilities;
using UnityEngine;

namespace Assets.Scripts.Weapons.Bows
{
    internal class PlayerBow : Weapon
    {
        [SerializeField] private Arrow _arrowPrefab;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Mark _mark;
        [SerializeField] private float _radius;

        private float _animationOffset = 0.5f;
        private bool _isOnCooldown = false;

        private Coroutine _attackCoroutine;
        private WaitForSeconds _shootSuspender;
        private WaitForSeconds _animationSuspender;
        private ArrowsPool _pool;
        private ClosestTargetFinder _closestTargetFinder;
        private IDamageable _closestTarget;
        private bool _haveTarget;

        public Transform Target => _haveTarget ? _closestTarget.Transform : null;

        private void Awake()
        {
            _closestTargetFinder = new ClosestTargetFinder(_radius, EnemyLayerMask);
            _pool = new ArrowsPool(_arrowPrefab, Damage, EnemyLayerMask);
            _shootSuspender = new WaitForSeconds(AttackSpeed - _animationOffset);
            _animationSuspender = new WaitForSeconds(_animationOffset);

            _mark.transform.SetParent(null);
        }

        private void FixedUpdate()
        {
            if (_closestTargetFinder.TryFindTarget(transform.position, out _closestTarget) && _closestTarget.Health > 0)
            {
                _haveTarget = true;
                _mark.MarkEnemy(_closestTarget);
                CanAttack = !_isOnCooldown;
            }
            else
            {
                _haveTarget = false;
                _mark.UnMarkEnemy();
                CanAttack = false;
            }
        }

        private void OnDisable()
        {
            if (_mark != null)
            {
                _mark.UnMarkEnemy();
            }
        }

        public override void Attack()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }

            _attackCoroutine = StartCoroutine(AttackDelay());
        }

        private IEnumerator AttackDelay()
        {
            Transform target = _closestTarget.Transform;

            _isOnCooldown = true;

            base.Attack();

            yield return _shootSuspender;

            Arrow arrow = _pool.GetArrow();

            arrow.transform.position = _shootPoint.position;
            arrow.Fly(target);

            yield return _animationSuspender;

            _isOnCooldown = false;
        }
    }
}