using Assets.Scripts.Audio;
using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Units.UnitFiniteStateMachine
{
    internal class FSMStateAttack : FSMState
    {
        private FiniteStateMachine _fsm;
        private IFSMControllable _unit;
        private Data _unitData;
        private UnitSFX _unitSFX;
        private Animator _animator;
        private float _distance;
        private float _timePast;

        public FSMStateAttack(
            FiniteStateMachine fsm,
            IFSMControllable unit,
            Animator animator,
            Data data,
            UnitSFX unitSFX)
        {
            _fsm = fsm;
            _unit = unit;
            _unitData = data;
            _unitSFX = unitSFX;
            _animator = animator;
        }

        public override void Update()
        {
            if (_fsm.Target != null && _fsm.Target.Health > 0 &&
                _fsm.Target.Transform.gameObject.activeSelf)
            {
                if (NeedChaseEnemy())
                {
                    _fsm.SetState<FSMStateChaseEnemy>();
                }
                else
                {
                    RotateTowards(_fsm.Target.Transform.position);
                    Attack();
                }
            }
            else
            {
                _fsm.SetState<FSMStateIdle>();
            }
        }

        private bool NeedChaseEnemy()
        {
            _distance = Vector3.Distance(_unit.Transform.position, _fsm.Target.Transform.position);

            if (_distance > _unitData.AttackRange)
                return true;

            return false;
        }

        private void Attack()
        {
            _timePast += Time.deltaTime;

            if (_timePast >= _unitData.AttackSpeed)
            {
                _unit.Attack(_fsm.Target);
                _unitSFX.PlayAttackSound();

                _animator.SetTrigger(AnimatorHash.Attack);

                _timePast = 0;
            }
        }

        private void RotateTowards(Vector3 targetPosition)
        {
            Vector3 directionToTarget = targetPosition - _unit.Transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

            _unit.Transform.rotation =
                Quaternion.Slerp(
                _unit.Transform.rotation,
                targetRotation,
                Time.fixedDeltaTime);
        }
    }
}