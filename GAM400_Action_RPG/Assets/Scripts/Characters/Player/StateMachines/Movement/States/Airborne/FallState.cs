using UnityEngine;

namespace BattleZZang
{
    public class FallState : PlayerAirborneState
    {
        private PlayerFallData fallData;
        private Vector3 enterPosition;

        public FallState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
            fallData = airborneData.FallData;
        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation(animationData.FallParameterHash);

            enterPosition = stateMachine.Player.transform.position;

            movementShareData.MoveSpeedModifier = 0.0f;

            animator.applyRootMotion = false;
            var currentVelocity = physics.RigidBody.velocity;
            Vector3 newVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
            physics.RigidBody.velocity = newVelocity;
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(animationData.FallParameterHash);

            animator.applyRootMotion = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            LimitVerticalVelocity();
        }

        protected override void OnContactWithGround(Collider collider)
        {
            var currentPosition = stateMachine.Player.transform.position;
            float fallDistance = Mathf.Abs(enterPosition.y - currentPosition.y);

            if (fallDistance < fallData.HardFallDistance)
            {
                stateMachine.Change(stateMachine.LightLand);
                return;
            }

            if (movementShareData.IsWalk && !movementShareData.IsSprint ||  movementShareData.MovementInput == Vector2.zero)
            {
                stateMachine.Change(stateMachine.HardLand);
                return;
            }

            stateMachine.Change(stateMachine.Roll);
        }

        protected override void ResetSprintState()
        {

        }

        private void LimitVerticalVelocity()
        {
            var currentVelocity = physics.RigidBody.velocity;

            if (currentVelocity.y >= -fallData.FallSpeedLimit)
                return;

            Vector3 newVelocity = new Vector3(0, -fallData.FallSpeedLimit - currentVelocity.y, 0.0f);
            physics.RigidBody.AddForce(-newVelocity, ForceMode.VelocityChange);
        }
    }
}
