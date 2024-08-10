using Assets.Scripts.GameLogic;
using UnityEngine;

namespace Assets.Scripts.EnemyComponents
{
    [CreateAssetMenu(fileName = "NewEnemy", menuName = "Data/EnemyData")]
    internal class EnemyData : Data
    {
        [SerializeField] private Enemy _prefab;

        public Enemy Prefab => _prefab;
    }
}