using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    internal abstract class Data : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private LayerMask _enemyLayerMask;
        [SerializeField] private float _health;
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _aggroRange;

        public string Name => _name;
        public LayerMask EnemyLayerMask => _enemyLayerMask;
        public float Health => _health;
        public float Speed => _speed;
        public float Damage => _damage;
        public float AttackSpeed => _attackSpeed;
        public float AttackRange => _attackRange;
        public float AggroRange => _aggroRange;
    }
}