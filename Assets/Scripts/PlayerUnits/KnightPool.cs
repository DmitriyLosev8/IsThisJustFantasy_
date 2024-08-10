using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts.PlayerUnits
{
    internal class KnightPool
    {
        private Unit[] _unitsPool;

        private int _capacity = 10;

        public KnightPool(UnitData data, Vector3 position)
        {
            _unitsPool = CreateUnitsPool(data, position);
        }

        public Unit[] MeleePool => _unitsPool;

        public Unit GetUnit()
        {
            foreach (var melee in _unitsPool)
            {
                if (melee.gameObject.activeSelf == false)
                {
                    melee.gameObject.SetActive(true);

                    return melee;
                }
            }

            throw new System.Exception("Not enough MeleeUnit in The pool!!!");
        }

        private Unit[] CreateUnitsPool(UnitData data, Vector3 position)
        {
            Unit[] pool = new Unit[_capacity];

            for (int i = 0; i < _capacity; i++)
            {
                Unit unit = GameObject.Instantiate(data.Prefab, position, Quaternion.identity);
                unit.Init(data);
                unit.gameObject.SetActive(false);
                pool[i] = unit;
            }

            return pool;
        }
    }
}