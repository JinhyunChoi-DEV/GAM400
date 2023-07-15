using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class PlayerGroundedState : PlayerMoveState
    {
        protected PlayerFeetGrounder feetIK;

        public PlayerGroundedState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
            feetIK = new PlayerFeetGrounder(stateMachine.Player);
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(animationData.GroundedParameterHash);

            UpdateIsSprintState();

            UpdateCameraRecentering(movementShareData.MovementInput);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(animationData.GroundedParameterHash);
        }

        private void UpdateIsSprintState()
        {
            if (!movementShareData.IsSprint)
                return;

            if (movementShareData.MovementInput != Vector2.zero)
                return;

            movementShareData.IsSprint = false;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            bool needRecentering = false;
            physics.UpdateFloating(ref needRecentering);

            if(needRecentering)
                UpdateCameraRecentering(movementShareData.MovementInput);

            feetIK.UpdateFeetPosition();
        }

        public override void OnAnimatorIK(int layerIndex)
        {
            base.OnAnimatorIK(layerIndex);

            feetIK.OnAnimatorIK(layerIndex);
        }

        protected override void AddInputActionCallback()
        {
            base.AddInputActionCallback();

            input.PlayerActions.Sprint.started += OnSprintStarted;
            input.PlayerActions.Jump.started += OnJumpStarted;
        }

        protected override void RemoveInputActionCallback()
        {
            base.RemoveInputActionCallback();

            input.PlayerActions.Sprint.started -= OnSprintStarted;
            input.PlayerActions.Jump.started -= OnJumpStarted;
        }

        private void OnSprintStarted(InputAction.CallbackContext obj)
        {
            stateMachine.Change(stateMachine.Sprint);
        }

        protected override void OnContactWithGroundExit(Collider collider)
        {
            base.OnContactWithGroundExit(collider);

            if (physics.IsGroundUnderneath())
                return;

            var capsuleCenter = physics.Collider.CapsuleColliderUtility.CapsuleColliderData.Collider.bounds.center;
            var newCenter = capsuleCenter - physics.Collider.CapsuleColliderUtility.CapsuleColliderData.ColliderVerticalExtents;
            var result = physics.GetRayResult(newCenter, Vector3.down, moveData.FallRayDistance);

            if(!result.IsCasted)
                OnFall();
        }

        protected virtual void OnMove()
        {
            if (movementShareData.IsSprint)
            {
                stateMachine.Change(stateMachine.Sprint);
                return;
            }

            if (movementShareData.IsWalk)
            {
                stateMachine.Change(stateMachine.Walk);
                return;
            }
            
            stateMachine.Change(stateMachine.Run);
        }

        protected virtual void OnJumpStarted(InputAction.CallbackContext context)
        {
            stateMachine.Change(stateMachine.Jump);
        }

        protected virtual void OnFall()
        {
            stateMachine.Change(stateMachine.Fall);
        }
    }
}
