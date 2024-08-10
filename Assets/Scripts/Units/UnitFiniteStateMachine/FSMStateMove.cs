using Assets.Scripts.Audio;
using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Units.UnitFiniteStateMachine
{
    internal class FSMStateMove : FSMState
    {
        private NavMeshAgent _agent;
        private UnitSFX _unitSFX;
        private Animator _animator;
        private Data _unitData;
        private FiniteStateMachine _fsm;
        private IFSMControllable _unit;
        private Vector3 _roundedUnitPos;
        private Vector3 _roundedTargetPos;
        private NavMeshPath _path;

        public FSMStateMove(
            FiniteStateMachine fsm,
            IFSMControllable unit,
            NavMeshAgent navMesh,
            Animator animator,
            Data data,
            UnitSFX unitSFX)
        {
            _fsm = fsm;
            _unitSFX = unitSFX;
            _animator = animator;
            _unitData = data;
            _agent = navMesh;
            _unit = unit;
            _path = new NavMeshPath();
        }

        public override void Enter()
        {
            _unitSFX.PlayWalkSound();
            _agent.speed = _unitData.Speed;
            _agent.CalculatePath(_fsm.MovePosition, _path);
            _agent.SetPath(_path);
            _animator.SetBool(AnimatorHash.Moving, true);
        }

        public override void Exit()
        {
            _animator.SetBool(AnimatorHash.Moving, false);
        }

        public override void Update()
        {
            if (HasArriveDestination(_unit.Transform.position, _agent.pathEndPosition))
            {
                _fsm.SetState<FSMStateIdle>();
            }
        }

        private bool HasArriveDestination(Vector3 position, Vector3 targetPosition)
        {
            _roundedUnitPos = new Vector3(
                Mathf.RoundToInt(position.x),
                Mathf.RoundToInt(position.y),
                Mathf.RoundToInt(position.z));

            _roundedTargetPos = new Vector3(
                Mathf.RoundToInt(targetPosition.x),
                Mathf.RoundToInt(targetPosition.y),
                Mathf.RoundToInt(targetPosition.z));

            return _roundedUnitPos == _roundedTargetPos;
        }
    }
}