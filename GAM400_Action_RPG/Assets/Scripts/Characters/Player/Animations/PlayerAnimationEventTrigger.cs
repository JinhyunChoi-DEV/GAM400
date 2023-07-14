using UnityEngine;

namespace BattleZZang
{
    public class PlayerAnimationEventTrigger : MonoBehaviour
    {
        [SerializeField]private Player player;

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

        public void TriggerOnMoveStateAnimationEnterEventDirect()
        {
            player.OnMoveStateAnimationEventEnter();
        }

        public void TriggerOnMoveStateAnimationExitDirect()
        {
            player.OnMoveStateAnimationEventExit();
        }

        public void TriggerOnMoveStateAnimationTransitionEventDirect()
        {
            player.OnMoveStateAnimationTransitionEvent();
        }

        private bool IsInAnimationTransition(int layerIndex = 0)
        {
            return player.Animator.IsInTransition(layerIndex);
        }
    }
}
