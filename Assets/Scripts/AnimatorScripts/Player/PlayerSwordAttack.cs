using Assets.Scripts.PlayerComponents;
using UnityEngine;

namespace Assets.Scripts.AnimatorScripts.Player
{
    internal class PlayerSwordAttack : StateMachineBehaviour
    {
        private PlayerMovement _movement;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_movement == null)
                _movement = animator.gameObject.GetComponent<PlayerMovement>();
            else
                _movement.SlowDown();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _movement.StartMove();
        }
    }
}