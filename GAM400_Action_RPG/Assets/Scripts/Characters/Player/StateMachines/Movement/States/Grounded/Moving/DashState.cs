using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class DashState : PlayerGroundedState
    {
        private PlayerDashData dashData;
        private float startTime;
        private int consecutiveDashUsed;
        private float dashToSprintTime = 1.0f;
        private bool isKeepRotate;

        public DashState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
            dashData = moveData.DashData;
        }

        public override void Enter()
        {
            movementShareData.MoveSpeedModifier = dashData.SpeedModifer;

            base.Enter();

            movementShareData.RotationData = dashData.RotationData;
            movementShareData.CurrentJumpForce = airborneData.JumpData.StrongForce;

            Dash();

            isKeepRotate = movementShareData.MovementInput != Vector2.zero;

            UpdateConsecutiveDashes();

            startTime = Time.time;
        }

        public override void Exit()
        {
            base.Exit();

            SetBaseRotationData();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!isKeepRotate)
                return;

            RotateByDirection();
        }

        public override void Update()
        {
            base.Update();

            if (Time.time < startTime + dashToSprintTime)
                return;

            stateMachine.Change(stateMachine.Sprint);
        }

        public override void OnAnimationTransition()
        {
            if (movementShareData.MovementInput == Vector2.zero)
            {
                stateMachine.Change(stateMachine.HardStop);
                return;
            }

            stateMachine.Change(stateMachine.Sprint);
        }

        protected override void AddInputActionCallback()
        {
            base.AddInputActionCallback();

            input.PlayerActions.Movement.performed += OnMovePerformed;
        }

        protected override void RemoveInputActionCallback()
        {
            base.RemoveInputActionCallback();

            input.PlayerActions.Movement.performed -= OnMovePerformed;
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            isKeepRotate = true;
        }

        protected override void OnDashStarted(InputAction.CallbackContext context)
        { }

        private void Dash()
        {
            Vector3 dashDirection = stateMachine.Player.transform.forward;
            dashDirection.y = 0.0f;

            UpdateRotationByDirect(dashDirection, false);

            if (movementShareData.MovementInput != Vector2.zero)
            {
                UpdateRotationByDirect(GetMoveInputDirection());
                dashDirection = GetMoveDirection();
            }

            var velocity = dashDirection * GetSpeed();
            physics.RigidBody.velocity = velocity;
        }

        private void UpdateConsecutiveDashes()
        {
            if (!IsConsecutive()) 
                consecutiveDashUsed = 0;
            
            consecutiveDashUsed++;

            if (consecutiveDashUsed == dashData.ConsecutiveLimitCount)
            {
                consecutiveDashUsed = 0;
                input.DisableActionFor(input.PlayerActions.Dash, dashData.DashLimitCooldown);
            }
        }

        private bool IsConsecutive()
        {
            return Time.time < startTime + dashData.TimeToConsideredConsecutive;
        }
    }
}
