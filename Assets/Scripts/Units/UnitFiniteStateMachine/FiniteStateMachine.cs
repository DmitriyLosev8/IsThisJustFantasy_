using System;
using System.Collections.Generic;
using Assets.Scripts.Audio;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Units.UnitFiniteStateMachine
{
    internal class FiniteStateMachine
    {
        private FSMState _currentState;
        private Dictionary<Type, FSMState> _states = new Dictionary<Type, FSMState>();

        public FiniteStateMachine(Animator animator, NavMeshAgent agent, IFSMControllable unit, Data data, UnitSFX unitSFX)
        {
            AddState(new FSMStateIdle(this, unit, data, unitSFX));
            AddState(new FSMStateMove(this, unit, agent, animator, data, unitSFX));
            AddState(new FSMStateChaseEnemy(this, agent, animator, data, unitSFX));
            AddState(new FSMStateAttack(this, unit, animator, data, unitSFX));
        }

        public Vector3 MovePosition { get; private set; }

        public IDamageable Target { get; private set; }

        public void AddState(FSMState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<T>()
            where T : FSMState
        {
            var type = typeof(T);

            if (_currentState?.GetType() == typeof(FSMStateMove) || _currentState?.GetType() != type)
            {
                if (_states.TryGetValue(type, out var newState))
                {
                    _currentState?.Exit();

                    _currentState = newState;

                    _currentState.Enter();
                }
            }
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void SetMovePosition(Vector3 position)
        {
            MovePosition = position;
        }

        public void SetTarget(IDamageable target)
        {
            Target = target;
        }
    }
}