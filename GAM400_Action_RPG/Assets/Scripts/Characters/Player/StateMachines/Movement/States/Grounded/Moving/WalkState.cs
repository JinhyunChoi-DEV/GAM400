using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class WalkState : PlayerMovingState
    {
        public WalkState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        { }

        public override void Enter()
        {
            movementShareData.MoveSpeedModifier = moveData.WalkData.SpeedModifier;
            movementShareData.BackwardsCameraRecenteringData = moveData.WalkData.BackwardsCameraRecenteringData;

            base.Enter();

            movementShareData.CurrentJumpForce = airborneData.JumpData.WeakForce;
        }

        public override void Exit()
        {
            base.Exit();

            SetBaseCameraRecentering();
        }

        public override void Update()
        {
            base.Update();

            animator.SetFloat(animationData.MoveSpeedParameterHash, 0.0f, 0.4f, Time.deltaTime);
        }

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.Change(stateMachine.Run);
        }

        protected override void OnMoveCanceled(InputAction.CallbackContext context)
        {
            stateMachine.Change(stateMachine.LightStop);

            base.OnMoveCanceled(context);
        }
    }
}
