using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class PlayerMoveState : IState
    {
        protected readonly PlayerMoveStateMachine stateMachine;
        protected readonly PlayerGroundedData moveData;
        protected PlayerInput input => stateMachine.Player.Input;
        protected PlayerPhysics physics => stateMachine.Player.Physics;
        protected PlayerCamera camera => stateMachine.Player.Camera;
        protected PlayerMovementShareData movementShareData => stateMachine.MovementShareData;

        public PlayerMoveState(PlayerMoveStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            moveData = stateMachine.Player.Data.GroundedData;

            Initialize();
        }

        private void Initialize()
        {
            movementShareData.TimeToReachTargetRotation = moveData.BaseRotationData.TargetRotationReachTime;
        }

        public virtual void Enter()
        {
            Debug.Log("State: " + GetType().Name);

            AddInputActionCallback();
        }

        public virtual void Exit()
        {
            RemoveInputActionCallback();
        }


        public virtual void HandleInput()
        {
            ReadMoveInput();
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
            Move();
        }

        //TODO: Should fix because all state was added this actions but it is not necessary
        protected virtual void AddInputActionCallback()
        {
            input.PlayerActions.WalkToggle.started += OnWalkToggleStarted;
        }

        protected virtual void RemoveInputActionCallback()
        {
            input.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;
        }

        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            movementShareData.IsWalk = !movementShareData.IsWalk;
        }

        private void ReadMoveInput()
        {
            movementShareData.MovementInput = input.PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void Move()
        {
            if (movementShareData.MovementInput == Vector2.zero || movementShareData.MoveSpeedModifier == 0.0f)
                return;

            Rotate(GetMoveInputDirection());
            var dir = GetMoveDirection();
            var speed = GetSpeed();
            physics.ApplyForce(dir, speed);
        }

        private void Rotate(Vector3 dir)
        {
            UpdateDirectAngle(dir);
            RotateByDirection();
        }
        protected void RotateByDirection()
        {
            float currentAngle = physics.GetRotation().eulerAngles.y;

            if (Math.Abs(currentAngle - movementShareData.CurrentTargetRotation.y) < MathVariables.epsilon)
                return;

            float adjustAngle = Mathf.SmoothDampAngle(currentAngle, movementShareData.CurrentTargetRotation.y, ref movementShareData.DampedTargetRotationCurrentVelocity.y, movementShareData.TimeToReachTargetRotation.y - movementShareData.DampedTargetRotationPassedTime.y);
            movementShareData.DampedTargetRotationPassedTime.y += Time.deltaTime;

            var targetRotation = Quaternion.Euler(0, adjustAngle, 0);
            physics.ApplyRotation(targetRotation);
        }

        #region Utiles
        protected Vector3 GetMoveInputDirection()
        {
            return new Vector3(movementShareData.MovementInput.x, 0, movementShareData.MovementInput.y);
        }

        protected Vector3 GetMoveDirection()
        {
            return Quaternion.Euler(0, movementShareData.CurrentTargetRotation.y, 0) * Vector3.forward;
        }

        protected float GetSpeed()
        {
            return moveData.BaseSpeed * movementShareData.MoveSpeedModifier * physics.PhysicsShareData.SlopeSpeedModifiers;
        }

        protected void UpdateDirectAngle(Vector3 dir, bool useCameraRotation = true)
        {
            float directAngle = GetMoveAngle(dir);

            if (useCameraRotation)
                ApplyCameraRotation(ref directAngle);

            if (Math.Abs(directAngle - movementShareData.CurrentTargetRotation.y) > MathVariables.epsilon)
                UpdateCurrentRotation(directAngle);
        }

        private void UpdateCurrentRotation(float directAngle)
        {
            movementShareData.CurrentTargetRotation.y = directAngle;
            movementShareData.DampedTargetRotationPassedTime.y = 0.0f;
        }

        private float GetMoveAngle(Vector3 direction)
        {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            if (directionAngle < 0.0f)
                directionAngle += 360.0f;

            return directionAngle;
        }

        private void ApplyCameraRotation(ref float angle)
        {
            angle += camera.CameraTransform.eulerAngles.y;

            if (angle > 360.0f)
                angle -= 360.0f;
        }

        #endregion
    }
}
