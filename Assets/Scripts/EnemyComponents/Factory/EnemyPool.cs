using Assets.Scripts.BuildingSystem.Buildings;
using UnityEngine;

namespace Assets.Scripts.EnemyComponents.Factory
{
    internal class EnemyPool
    {
        private Enemy[] _enemyPool;
        private int _capacity = 15;

        public EnemyPool(EnemyData data, MainBuilding building, Vector3 position)
        {
            _enemyPool = CreateUnitsPool(data, building, position);
        }

        public Enemy GetEnemy()
        {
            foreach (var melee in _enemyPool)
            {
                if (melee.gameObject.activeSelf == false)
                {
                    melee.gameObject.SetActive(true);

                    return melee;
                }
            }

            throw new System.Exception("Not enough units in The pool!!!");
        }

        private Enemy[] CreateUnitsPool(EnemyData data, MainBuilding building, Vector3 position)
        {
            Enemy[] pool = new Enemy[_capacity];

            for (int i = 0; i < _capacity; i++)
            {
                Enemy unit = GameObject.Instantiate(data.Prefab, position, Quaternion.identity);
                unit.Init(data);
                unit.GetComponent<EnemyMovement>().InitTarget(building);
                unit.gameObject.SetActive(false);
                pool[i] = unit;
            }

            return pool;
        }
    }
}