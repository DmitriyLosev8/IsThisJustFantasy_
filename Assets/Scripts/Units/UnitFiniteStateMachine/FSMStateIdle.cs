using Assets.Scripts.Audio;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.Interfaces;
using Assets.Scripts.GameLogic.Utilities;

namespace Assets.Scripts.Units.UnitFiniteStateMachine
{
    internal class FSMStateIdle : FSMState
    {
        private FiniteStateMachine _fsm;
        private IFSMControllable _unit;
        private UnitSFX _unitSFX;
        private ClosestTargetFinder _targetFinder;
        private IDamageable _target;

        public FSMStateIdle(FiniteStateMachine fsm, IFSMControllable unit, Data data, UnitSFX unitSFX)
        {
            _fsm = fsm;
            _unitSFX = unitSFX;
            _unit = unit;

            _targetFinder = new ClosestTargetFinder(data.AggroRange, data.EnemyLayerMask);
        }

        public override void Enter()
        {
            _unitSFX.Stop();
            _fsm.SetTarget(null);
        }

        public override void Update()
        {
            if (_targetFinder.TryFindTarget(_unit.Transform.position, out _target))
            {
                _fsm.SetTarget(_target);
                _fsm.SetState<FSMStateChaseEnemy>();
            }
        }
    }
}