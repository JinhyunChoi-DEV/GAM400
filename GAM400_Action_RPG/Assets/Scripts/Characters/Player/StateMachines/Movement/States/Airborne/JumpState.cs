using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class JumpState : PlayerAirborneState
    {
        private PlayerJumpData jumpData;
        private bool isKeepRotate;
        private bool isFalling;

        public JumpState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
            jumpData = airborneData.JumpData;
        }

        public override void Enter()
        {
            base.Enter();

            movementShareData.RotationData = jumpData.RotationData;
            movementShareData.MoveDecelerationForce = jumpData.DecelerationForce;
            movementShareData.MoveSpeedModifier = 0.0f;

            isKeepRotate = movementShareData.MovementInput != Vector2.zero;

            animator.applyRootMotion = false;
            Jump();
        }
        public override void Exit()
        {
            base.Exit();

            SetBaseRotationData();

            animator.applyRootMotion = true;
            isFalling = false;
        }

        public override void Update()
        {
            base.Update();

            if (!isFalling && physics.IsMovingUp(0.0f))
                isFalling = true;

            var currentVelocity = physics.RigidBody.velocity;
            if (!isFalling || currentVelocity.y > 0)
                return;
            
            stateMachine.Change(stateMachine.Fall);
        }

        protected override void ResetSprintState()
        { }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (isKeepRotate)
                RotateByDirection();

            if(physics.IsMovingUp())
                DecelerateVertically();

        }

        private void Jump()
        {
            Vector3 jumpForce = movementShareData.CurrentJumpForce;

            Vector3 jumpDirection = stateMachine.Player.transform.forward;

            if (isKeepRotate)
            {
                UpdateRotationByDirect(GetMoveInputDirection());
                jumpDirection = GetMoveDirection();
            }

            jumpForce.x *= jumpDirection.x;
            jumpForce.z *= jumpDirection.z;

            var center = physics.Collider.CapsuleColliderUtility.CapsuleColliderData.Collider.bounds.center;
            var result = physics.GetRayResult(center , Vector3.down, jumpData.JumpToGroundRayDistance);

            if (result.IsCasted)
            {
                if (physics.IsMovingUp())
                {
                    float forceModifer = jumpData.JumpForceModiferBySlopeMoveUp.Evaluate(result.GroundAngle);

                    jumpForce.x *= forceModifer;
                    jumpForce.z *= forceModifer;
                }

                if (physics.IsMovingDown())
                {
                    float forceModifer = jumpData.JumpForceModiferBySlopeMoveDown.Evaluate(result.GroundAngle);

                    jumpForce.y *= forceModifer;
                }
            }

            ResetVelocity();

            physics.RigidBody.AddForce(jumpForce, ForceMode.VelocityChange);
        }

        protected override void OnMoveCanceled(InputAction.CallbackContext context)
        { }
    }
}
