using Assets.Scripts.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.PlayerUnits
{
    internal class KnightFactory : MonoBehaviour
    {
        [SerializeField] private UnitData _unitData;
        [SerializeField] private Transform _spotOfRespawnUnits;

        private KnightPool _pool;

        public void Init(SelectedUnitsHandler handler)
        {
            _pool = new KnightPool(_unitData, transform.position);
            handler.Init(_pool.MeleePool);
        }

        public void Spawn()
        {
            Unit unit = _pool.GetUnit();
            NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
            agent.Warp(_spotOfRespawnUnits.transform.position);
        }
    }
}