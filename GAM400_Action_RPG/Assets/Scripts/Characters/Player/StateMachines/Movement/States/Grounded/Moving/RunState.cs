using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class RunState : PlayerMovingState
    {
        public RunState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            movementShareData.MoveSpeedModifier = moveData.RunData.SpeedModifier;
        }

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

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.Change(stateMachine.Walk);
        }
    }
}
