using UnityEngine;

namespace Assets.Scripts.GameLogic.Interfaces
{
    internal interface IFSMControllable
    {
        public Transform Transform { get; }

        void Attack(IDamageable target);
    }
}