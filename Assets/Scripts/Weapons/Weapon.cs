using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    internal abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private LayerMask _enemyLayerMask;
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private float _damage;
        [SerializeField] private float _attackSpeed;

        private Coroutine AttackCoroutine;
        private WaitForSeconds _attackDelay;

        public bool CanAttack { get; protected set; }

        public float Damage => _damage;

        public float AttackSpeed => _attackSpeed;

        public LayerMask EnemyLayerMask => _enemyLayerMask;

        private void OnEnable()
        {
            CanAttack = true;
        }

        private void Start()
        {
            _attackDelay = new WaitForSeconds(_attackSpeed);
        }

        public virtual void Attack()
        {
            if (CanAttack)
                AttackCoroutine = StartCoroutine(AttackDelay(_attackSpeed));
        }

        public void LevelUp(float damage, float attackSpeed)
        {
            _damage += damage;
            _attackSpeed -= attackSpeed;
        }

        private IEnumerator AttackDelay(float attackSpeed)
        {
            _audioSource.Play();
            CanAttack = false;

            yield return _attackDelay;

            CanAttack = true;
        }
    }
}