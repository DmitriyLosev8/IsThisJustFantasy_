using Assets.Scripts.Audio;
using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Units.UnitFiniteStateMachine
{
    internal class FSMStateChaseEnemy : FSMState
    {
        private NavMeshAgent _agent;
        private UnitSFX _unitSFX;
        private Animator _animator;
        private FiniteStateMachine _fsm;
        private Data _unitData;
        private NavMeshPath _path;

        public FSMStateChaseEnemy(
            FiniteStateMachine fsm,
            NavMeshAgent navMesh,
            Animator animator,
            Data data,
            UnitSFX unitSFX)
        {
            _agent = navMesh;
            _unitSFX = unitSFX;
            _animator = animator;
            _fsm = fsm;
            _unitData = data;
            _path = new NavMeshPath();
        }

        public override void Enter()
        {
            _unitSFX.PlayWalkSound();
            _agent.CalculatePath(_fsm.Target.Transform.position, _path);
            _agent.SetPath(_path);
            _animator.SetBool(AnimatorHash.Moving, true);
        }

        public override void Exit()
        {
            _agent.ResetPath();
            _animator.SetBool(AnimatorHash.Moving, false);
        }

        public override void Update()
        {
            if (_agent.remainingDistance > _unitData.AggroRange)
                _fsm.SetState<FSMStateIdle>();

            if (_agent.remainingDistance <= _unitData.AttackRange)
            {
                _fsm.SetState<FSMStateAttack>();
            }
        }
    }
}