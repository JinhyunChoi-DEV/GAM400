using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class SprintState : PlayerMovingState
    {
        private PlayerSprintData sprintData;
        private bool isSprint;

        public SprintState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
            sprintData = moveData.SprintData;
        }

        public override void Enter()
        {
            movementShareData.MoveSpeedModifier = sprintData.SpeedModifier;
            isSprint = true;

            base.Enter();

            movementShareData.CurrentJumpForce = airborneData.JumpData.StrongForce;
        }

        public override void Exit()
        {
            base.Exit();

            isSprint = false;
        }

        public override void Update()
        {
            base.Update();

            animator.SetFloat(animationData.MoveSpeedParameterHash, 1.0f, 0.4f, Time.deltaTime);

            if (isSprint)
                return;

            StopSprint();
        }

        private void StopSprint()
        {
            if (movementShareData.MovementInput == Vector2.zero)
            {
                stateMachine.Change(stateMachine.Idle);
                return;
            }

            stateMachine.Change(stateMachine.Run);
        }

        protected override void AddInputActionCallback()
        {
            base.AddInputActionCallback();

            input.PlayerActions.Sprint.canceled += FinishSprint;
        }

        protected override void RemoveInputActionCallback()
        {
            base.RemoveInputActionCallback();

            input.PlayerActions.Sprint.canceled -= FinishSprint;
        }

        private void FinishSprint(InputAction.CallbackContext obj)
        {
            isSprint = false;
        }

        protected override void OnMoveCanceled(InputAction.CallbackContext context)
        {
            stateMachine.Change(stateMachine.HardStop);

            base.OnMoveCanceled(context);
        }
    }
}
