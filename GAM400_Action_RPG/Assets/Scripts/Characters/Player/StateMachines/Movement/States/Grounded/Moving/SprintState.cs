using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class SprintState : PlayerMovingState
    {
        private PlayerSprintData sprintData;

        private float startTime;
        private bool isSprint;
        private bool needToReset;

        public SprintState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
            sprintData = moveData.SprintData;
        }

        public override void Enter()
        {
            movementShareData.MoveSpeedModifier = sprintData.SpeedModifier;

            base.Enter();

            movementShareData.CurrentJumpForce = airborneData.JumpData.StrongForce;
            needToReset = true;
            startTime = Time.time;
        }

        public override void Exit()
        {
            base.Exit();

            if (needToReset)
            {
                isSprint = false;
                movementShareData.IsSprint = false;
            }
        }

        public override void Update()
        {
            base.Update();

            if (isSprint)
                return;


            if (Time.time < startTime + sprintData.SprintTime)
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

            input.PlayerActions.Sprint.performed += OnSprint;
        }

        protected override void RemoveInputActionCallback()
        {
            base.RemoveInputActionCallback();

            input.PlayerActions.Sprint.performed -= OnSprint;
        }

        private void OnSprint(InputAction.CallbackContext obj)
        {
            isSprint = true;
            movementShareData.IsSprint = true;
        }

        protected override void OnMoveCanceled(InputAction.CallbackContext context)
        {
            stateMachine.Change(stateMachine.HardStop);

            base.OnMoveCanceled(context);
        }

        protected override void OnJumpStarted(InputAction.CallbackContext context)
        {
            base.OnJumpStarted(context);

            needToReset = false;
        }

        protected override void OnFall()
        {
            needToReset = false;
            base.OnFall();
        }
    }
}
