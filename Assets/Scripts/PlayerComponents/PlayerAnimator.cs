using System.Collections;
using Assets.Scripts.AnimatorScripts;
using Assets.Scripts.Constants;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.PlayerComponents
{
    [RequireComponent(typeof(Animator))]
    internal class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private AnimatorTriggerConfiguration _triggerConfig;
        private AnimatorClipInfo[] _currentClipInfo;

        private int _numerator = 1;
        private float _currentClipLength;
        private float _animationUpdateTime = 0.5f;

        private Coroutine _animatorUpdate;
        private WaitForSeconds _suspender;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _triggerConfig = new AnimatorTriggerConfiguration();
            _suspender = new WaitForSeconds(_animationUpdateTime);
        }

        public void SetAnimatorSpeed(Vector3 movementVector, float moveSpeed)
        {
            float speed = Vector3.Magnitude(movementVector * moveSpeed);

            _animator.SetFloat(AnimatorHash.Speed, speed);
        }

        public void SetAnimatorAttackTrigger(Weapon weapon)
        {
            if (_animatorUpdate != null)
            {
                StopCoroutine(_animatorUpdate);
            }

            _animatorUpdate = StartCoroutine(AnimatorUpdate(weapon));
        }

        public void SetAnimatorChangeWeaponTrigger(Weapon weapon)
        {
            _animator.SetTrigger(_triggerConfig.GetChangeWeaponTrigger(weapon.GetType()));
        }

        private IEnumerator AnimatorUpdate(Weapon weapon)
        {
            _animator.SetTrigger(_triggerConfig.GetAttackTrigger(weapon.GetType()));

            _animator.Update(0);

            yield return _suspender;

            SetCurrentClipInfo();

            _animator.SetFloat(AnimatorHash.AttackSpeed, CalculateAnimationSpeed(weapon));
        }

        private void SetCurrentClipInfo()
        {
            _currentClipInfo = _animator.GetCurrentAnimatorClipInfo(0);

            if (_currentClipInfo[0].clip != null)
            {
                _currentClipLength = _currentClipInfo[0].clip.length;
            }
        }

        private float CalculateAnimationSpeed(Weapon weapon)
        {
            return _numerator / (weapon.AttackSpeed / _currentClipLength);
        }
    }
}