using System;
using System.Collections;
using Assets.Scripts.Audio;
using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.Interfaces;
using Assets.Scripts.Units.UnitFiniteStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Units
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    internal abstract class Unit : Selectable, IDamageable, IFSMControllable, IHealthDisplayable
    {
        [SerializeField] private AudioSource _audioSource;

        private float _health;
        private float _deathDuration = 5f;
        private FiniteStateMachine _fsm;
        private UnitSFX _unitSFX;
        private Coroutine _deathCoroutine;

        private NavMeshAgent _agent;
        private Animator _animator;
        private Data _data;

        public event Action<float> HealthValueChanged;

        public Transform Transform => transform;

        public float Health => _health;

        public FiniteStateMachine FSM => _fsm;

        public void Update()
        {
            if (_health <= 0)
                return;

            _fsm.Update();
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;

            HealthValueChanged?.Invoke(_health);

            if (_health <= 0)
                Die();
        }

        public void Init(Data data)
        {
            _data = data;
            _health = data.Health;
            HealthValueChanged?.Invoke(_health);

            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _unitSFX = GetComponentInChildren<UnitSFX>();

            _fsm = new FiniteStateMachine(_animator, _agent, this, _data, _unitSFX);

            _fsm.SetState<FSMStateIdle>();
        }

        public virtual void Attack(IDamageable target)
        {
            if (_audioSource != null)
                _audioSource.Play();

            target.TakeDamage(_data.Damage);
        }

        public virtual void Die()
        {
            if (_deathCoroutine != null)
            {
                StopCoroutine(_deathCoroutine);
            }

            _deathCoroutine = StartCoroutine(Death(_deathDuration));
        }

        private IEnumerator Death(float time)
        {
            _unitSFX.PlayDeathSound();
            _fsm.SetTarget(null);
            _agent.ResetPath();
            _animator.SetTrigger(AnimatorHash.Death);

            yield return new WaitForSeconds(time);

            _health = _data.Health;
            HealthValueChanged?.Invoke(_health);
            gameObject.SetActive(false);
        }
    }
}