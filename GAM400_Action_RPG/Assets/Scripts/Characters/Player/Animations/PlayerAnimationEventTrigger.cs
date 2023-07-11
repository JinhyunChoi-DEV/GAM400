using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleZZang
{
    public class PlayerAnimationEventTrigger : MonoBehaviour
    {
        private Player player;

        private void Awake()
        {
            player = transform.parent.GetComponent<Player>();
        }

        public void TriggerOnMoveStateAnimationEnterEvent()
        {
            if (IsInAnimationTransition())
                return;

            player.OnMoveStateAnimationEventEnter();
        }

        public void TriggerOnMoveStateAnimationExitEvent()
        {
            if (IsInAnimationTransition())
                return;

            player.OnMoveStateAnimationEventExit();
        }

        public void TriggerOnMoveStateAnimationTransitionEvent()
        {
            if (IsInAnimationTransition())
                return;

            player.OnMoveStateAnimationTransitionEvent();
        }

        private bool IsInAnimationTransition(int layerIndex = 0)
        {
            return player.Animator.IsInTransition(layerIndex);
        }
    }
}
