using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class PlayerGroundedState : PlayerMoveState
    {
        public PlayerGroundedState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        { }

        protected override void AddInputActionCallback()
        {
            base.AddInputActionCallback();

            stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
        }

        protected override void RemoveInputActionCallback()
        {
            base.RemoveInputActionCallback();

            stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        }

        protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.Change(stateMachine.Idle);
        }

        protected virtual void OnMove()
        {
            if (movementShareData.IsWalk)
            {
                stateMachine.Change(stateMachine.Walk);
                return;
            }
            
            stateMachine.Change(stateMachine.Run);
        }
    }
}
