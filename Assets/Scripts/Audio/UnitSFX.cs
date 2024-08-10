using System.Collections.Generic;
using Assets.Scripts.GameLogic;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    internal class UnitSFX : SFX
    {
        [SerializeField] private List<AudioClip> _attack;
        [SerializeField] private List<AudioClip> _death;
        [SerializeField] private List<AudioClip> _walk;

        public void PlayDeathSound() => PlaySound(_death);

        public void PlayAttackSound() => PlaySound(_attack);

        public void PlayWalkSound() => PlaySound(_walk, true);
    }
}