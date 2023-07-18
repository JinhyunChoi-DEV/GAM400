using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class PlayerGroundedState : PlayerMoveState
    {
        public PlayerGroundedState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        { }

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
        }

        public override void OnAnimatorIK(int layerIndex)
        {
            base.OnAnimatorIK(layerIndex);


            if(stateMachine.Player.ActiveIK)
                UpdateFeetIK();
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

        private void UpdateFeetIK()
        {
            var transform = stateMachine.Player.transform;

            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
            RaycastHit hit;
            Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, moveData.FeetIKOffset + 1.0f, physics.LayerData.GroundLayer))
            {
                Vector3 footPosition = hit.point;
                footPosition.y += moveData.FeetIKOffset;
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                Vector3 forward = Vector3.ProjectOnPlane(transform.forward, hit.normal);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(forward, hit.normal));
            }

            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
            ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, moveData.FeetIKOffset + 1.0f, physics.LayerData.GroundLayer))
            {
                Vector3 footPosition = hit.point;
                footPosition.y += moveData.FeetIKOffset;
                animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                Vector3 forward = Vector3.ProjectOnPlane(transform.forward, hit.normal);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(forward, hit.normal));
            }
        }
    }
}
