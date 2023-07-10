using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class HardLandState : PlayerLandState
    {
        public HardLandState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            movementShareData.MoveSpeedModifier = 0.0f;

            base.Enter();

            input.PlayerActions.Movement.Disable();

            ResetVelocity();
        }

        public override void Exit()
        {
            base.Exit();

            input.PlayerActions.Movement.Enable();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!IsMoveHorizontal())
                return;

            ResetVelocity();
        }

        public override void OnAnimationTransition()
        {
            stateMachine.Change(stateMachine.Idle);
        }

        public override void OnAnimationExit()
        {
            input.PlayerActions.Movement.Enable();
        }

        protected override void AddInputActionCallback()
        {
            base.AddInputActionCallback();

            input.PlayerActions.Movement.started += OnMoveStart;
        }

        protected override void RemoveInputActionCallback()
        {
            base.RemoveInputActionCallback();

            input.PlayerActions.Movement.started -= OnMoveStart;
        }


        protected override void OnMove()
        {
            if (stateMachine.MovementShareData.IsWalk)
                return;

            stateMachine.Change(stateMachine.Run);
        }

        protected override void OnJumpStarted(InputAction.CallbackContext context)
        {

        }

        private void OnMoveStart(InputAction.CallbackContext context)
        {
            OnMove();
        }
    }
}
