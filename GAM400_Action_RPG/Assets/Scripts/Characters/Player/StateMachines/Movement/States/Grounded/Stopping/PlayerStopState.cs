using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class PlayerStopState : PlayerGroundedState
    {
        public PlayerStopState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        { }

        public override void Enter()
        {
            movementShareData.MoveSpeedModifier = 0.0f;
            SetBaseCameraRecentering();

            base.Enter();

            StartAnimation(animationData.StopParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(animationData.StopParameterHash);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            RotateByDirection();

            if (!IsMoveHorizontal())
                return;

            DecelerateHorizontally();
        }

        public override void OnAnimationTransition()
        {
            stateMachine.Change(stateMachine.Idle);
        }

        protected override void AddInputActionCallback()
        {
            base.AddInputActionCallback();

            input.PlayerActions.Movement.started += OnMoveStarted;
        }

        protected override void RemoveInputActionCallback()
        {
            base.RemoveInputActionCallback();

            input.PlayerActions.Movement.started -= OnMoveStarted;
        }

        private void OnMoveStarted(InputAction.CallbackContext context)
        {
            OnMove();
        }
    }
}
