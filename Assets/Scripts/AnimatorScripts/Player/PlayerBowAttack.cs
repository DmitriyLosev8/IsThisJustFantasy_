using Assets.Scripts.PlayerComponents;
using Assets.Scripts.Weapons.Bows;
using UnityEngine;

namespace Assets.Scripts.AnimatorScripts.Player
{
    internal class PlayerBowAttack : StateMachineBehaviour
    {
        private PlayerBow _bow;
        private PlayerMovement _movement;
        private Vector3 _rotationOffset = new Vector3(0, 75, 0);

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_movement == null || _bow == null)
            {
                if (animator.TryGetComponent(out PlayerMovement movement) &&
                    animator.GetComponentInChildren<PlayerBow>() != null)
                {
                    _movement = movement;
                    _bow = animator.GetComponentInChildren<PlayerBow>();
                }
            }

            if (_movement != null)
            {
                _movement.SlowDown();
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_bow.Target != null)
                _movement.RotateTowards(_bow.Target, _rotationOffset);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _movement.StartMove();
        }
    }
}