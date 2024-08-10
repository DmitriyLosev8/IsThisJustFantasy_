using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.GameLogic;
using UnityEngine;

namespace Assets.Scripts.PlayerComponents
{
    internal class PlayerSFX : SFX
    {
        [SerializeField] private List<AudioClip> _heal;
        [SerializeField] private List<AudioClip> _changeWeapon;
        [SerializeField] private List<AudioClip> _takeHit;
        [SerializeField] private List<AudioClip> _walk;

        private Coroutine _walking;
        private bool _isPlayingWalkSound;
        private WaitForSeconds _playingTime;

        private float _highestPlayTime = 1.15f;
        private float _reduction = 10;

        public void PlayChangeWeaponSound() => PlaySound(_changeWeapon);

        public void PlayWalkSound(float speed)
        {
            if (_isPlayingWalkSound == false)
            {
                float playTime = _highestPlayTime - speed / _reduction;
                _playingTime = new WaitForSeconds(playTime);

                _walking = StartCoroutine(Walking());
            }
        }

        public void PlayTakeHitSound() => PlaySound(_takeHit);

        public void PlayHealSound() => PlaySound(_heal);

        private IEnumerator Walking()
        {
            PlaySound(_walk);

            _isPlayingWalkSound = true;

            yield return _playingTime;

            _isPlayingWalkSound = false;
        }
    }
}