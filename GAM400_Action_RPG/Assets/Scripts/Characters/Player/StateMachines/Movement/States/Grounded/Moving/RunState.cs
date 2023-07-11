using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class RunState : PlayerMovingState
    {
        private PlayerSprintData sprintData;

        private float startTime;

        public RunState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
            sprintData = moveData.SprintData;
        }

        public override void Enter()
        {
            movementShareData.MoveSpeedModifier = moveData.RunData.SpeedModifier;

            base.Enter();
            StartAnimation(animationData.RunParameterHash);

            movementShareData.CurrentJumpForce = airborneData.JumpData.MediumForce;

            startTime = Time.time;
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(animationData.RunParameterHash);
        }

        public override void Update()
        {
            base.Update();

            if (!movementShareData.IsWalk)
                return;

            if (Time.time < startTime + sprintData.RunToWalkTime)
                return;

            StopRun();
        }

        private void StopRun()
        {
            if (movementShareData.MovementInput == Vector2.zero)
            {
                stateMachine.Change(stateMachine.Idle);
                return;
            }

            stateMachine.Change(stateMachine.Walk);
        }

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.Change(stateMachine.Walk);
        }

        protected override void OnMoveCanceled(InputAction.CallbackContext context)
        {
            stateMachine.Change(stateMachine.MediumStop);

            base.OnMoveCanceled(context);
        }
    }
}
