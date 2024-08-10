using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.PlayerComponents
{
    [RequireComponent(typeof(PlayerAnimator))]
    internal class PlayerAttacker : PlayerComponent
    {
        [SerializeField] private List<Weapon> _weapons;

        private PlayerSFX _playerSFX;
        private PlayerAnimator _animator;
        private Weapon _currentWeapon;
        private bool _isChangingWeapon;
        private float _changeWeaponDelay = 1.5f;
        private WaitForSeconds _delay;

        public event Action WeaponChanged;

        private void Start()
        {
            _animator = GetComponent<PlayerAnimator>();
            _delay = new WaitForSeconds(_changeWeaponDelay);
            _isChangingWeapon = false;

            _currentWeapon = _weapons[0];
            _currentWeapon.gameObject.SetActive(true);
        }

        public void Attack()
        {
            if (_currentWeapon.CanAttack && _isChangingWeapon == false)
            {
                _animator.SetAnimatorAttackTrigger(_currentWeapon);

                _currentWeapon.Attack();
            }
        }

        public void ChangeWeapon()
        {
            if (_isChangingWeapon == true)
                return;

            _currentWeapon.gameObject.SetActive(false);

            int currentIndex = _weapons.IndexOf(_currentWeapon);
            int nextIndex = (currentIndex + 1) % _weapons.Count;

            _currentWeapon = _weapons[nextIndex];
            _currentWeapon.gameObject.SetActive(true);

            _animator.SetAnimatorChangeWeaponTrigger(_currentWeapon);
            _playerSFX.PlayChangeWeaponSound();

            WeaponChanged?.Invoke();
            StartCoroutine(ChangeWeaponDelay());
        }

        public override void Init(PlayerData level, PlayerSFX sfx)
        {
            _playerSFX = sfx;

            foreach (var weapon in _weapons)
            {
                weapon.LevelUp(level.WeaponDamageLevelUpAmount, level.WeaponAttackSpeedLevelUpAmount);
            }
        }

        private IEnumerator ChangeWeaponDelay()
        {
            _isChangingWeapon = true;

            yield return _delay;

            _isChangingWeapon = false;
        }
    }
}