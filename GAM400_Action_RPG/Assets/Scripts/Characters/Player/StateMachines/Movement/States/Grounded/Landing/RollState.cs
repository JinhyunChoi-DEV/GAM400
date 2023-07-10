using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class RollState : PlayerLandState
    {
        private PlayerRollData rollData;

        public RollState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
            rollData = moveData.RollData;
        }

        public override void Enter()
        {
            movementShareData.MoveSpeedModifier = rollData.SpeedModifer;

            base.Enter();

            movementShareData.IsSprint = false;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (movementShareData.MovementInput != Vector2.zero)
                return;

            RotateByDirection();
        }

        public override void OnAnimationTransition()
        {
            if (movementShareData.MovementInput == Vector2.zero)
            {
                stateMachine.Change(stateMachine.MediumStop);
                return;
            }

            OnMove();
        }

        protected override void OnJumpStarted(InputAction.CallbackContext context)
        { }
    }
}
