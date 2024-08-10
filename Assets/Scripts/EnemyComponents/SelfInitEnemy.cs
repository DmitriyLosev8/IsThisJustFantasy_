using UnityEngine;

namespace Assets.Scripts.EnemyComponents
{
    [RequireComponent(typeof(Enemy))]
    internal class SelfInitEnemy : MonoBehaviour
    {
        [SerializeField] private EnemyData _enemyData;

        private Enemy _enemy;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();

            _enemy.Init(_enemyData);
        }
    }
}
