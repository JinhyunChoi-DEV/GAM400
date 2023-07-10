using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class PlayerMoveState : IState
    {
        protected readonly PlayerMoveStateMachine stateMachine;
        protected readonly PlayerGroundedData moveData;
        protected readonly PlayerAirborneData airborneData;

        protected PlayerInput input => stateMachine.Player.Input;
        protected PlayerPhysics physics => stateMachine.Player.Physics;
        protected PlayerCamera camera => stateMachine.Player.Camera;
        protected PlayerMovementShareData movementShareData => stateMachine.MovementShareData;

        public PlayerMoveState(PlayerMoveStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;

            moveData = stateMachine.Player.Data.GroundedData;
            airborneData = stateMachine.Player.Data.AirborneData;

            SetBaseCameraRecentering();

            Initialize();
        }

        private void Initialize()
        {
            SetBaseRotationData();
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

        public virtual void OnAnimationEnter()
        { }

        public virtual void OnAnimationExit()
        { }

        public virtual void OnAnimationTransition()
        { }

        public virtual void OnTriggerEnter(Collider collider)
        {
            if (physics.IsGroundLayer(collider.gameObject.layer))
            {
                OnContactWithGround(collider);
                return;
            }
        }

        public void OnTriggerExit(Collider collider)
        {
            if (physics.IsGroundLayer(collider.gameObject.layer))
            {
                OnContactWithGroundExit(collider);
                return;
            }
        }

        protected virtual void AddInputActionCallback()
        {
            input.PlayerActions.WalkToggle.started += OnWalkToggleStarted;
            input.PlayerActions.Movement.canceled += OnMoveCanceled;
            input.PlayerActions.Movement.performed+= OnMovePerformed;
            input.PlayerActions.Look.started += OnMouseMoveStarted;
        }

        protected virtual void RemoveInputActionCallback()
        {
            input.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;
            input.PlayerActions.Movement.canceled -= OnMoveCanceled;
            input.PlayerActions.Movement.performed -= OnMovePerformed;
            input.PlayerActions.Look.started -= OnMouseMoveStarted;
        }

        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            movementShareData.IsWalk = !movementShareData.IsWalk;
        }

        protected void RotateByDirection()
        {
            float currentAngle = physics.RigidBody.rotation.eulerAngles.y;

            if (Math.Abs(currentAngle - movementShareData.CurrentTargetRotation.y) < MathVariables.epsilon)
                return;

            float adjustAngle = Mathf.SmoothDampAngle(currentAngle, movementShareData.CurrentTargetRotation.y, ref movementShareData.DampedTargetRotationCurrentVelocity.y, movementShareData.TimeToReachTargetRotation.y - movementShareData.DampedTargetRotationPassedTime.y);
            movementShareData.DampedTargetRotationPassedTime.y += Time.deltaTime;

            var targetRotation = Quaternion.Euler(0, adjustAngle, 0);
            physics.RigidBody.MoveRotation(targetRotation);
        }

        protected Vector3 GetMoveInputDirection()
        {
            return new Vector3(movementShareData.MovementInput.x, 0, movementShareData.MovementInput.y);
        }

        protected Vector3 GetMoveDirection()
        {
            return Quaternion.Euler(0, movementShareData.CurrentTargetRotation.y, 0) * Vector3.forward;
        }

        // the lecture use addSlope because it is same speed even if moving down, but my version diff
        protected float GetSpeed(/*bool addSlope= true*/)
        {
            return moveData.BaseSpeed * movementShareData.MoveSpeedModifier * physics.PhysicsShareData.SlopeSpeedModifiers;
        }

        protected void UpdateRotationByDirect(Vector3 dir, bool useCameraRotation = true)
        {
            float directAngle = GetMoveAngle(dir);

            if (useCameraRotation)
                ApplyCameraRotation(ref directAngle);

            if (Math.Abs(directAngle - movementShareData.CurrentTargetRotation.y) > MathVariables.epsilon)
                UpdateCurrentRotation(directAngle);
        }

        protected void ResetVelocity()
        {
            physics.RigidBody.velocity = Vector3.zero;
        }

        protected Vector3 GetHorizontalVelocity()
        {
            Vector3 velocity = physics.RigidBody.velocity;
            velocity.y = 0.0f;

            return velocity;
        }

        protected void DecelerateHorizontally()
        {
            Vector3 horizontalVelocity = GetHorizontalVelocity();
            var newForce = -horizontalVelocity * movementShareData.MoveDecelerationForce;
            physics.RigidBody.AddForce(newForce, ForceMode.Acceleration);
        }

        protected void DecelerateVertically()
        {
            Vector3 verticalVelocity = new Vector3(0, physics.RigidBody.velocity.y, 0);
            var newForce = -verticalVelocity * movementShareData.MoveDecelerationForce;
            physics.RigidBody.AddForce(newForce, ForceMode.Acceleration);
        }

        protected bool IsMoveHorizontal(float minMagnitude = 0.1f)
        {
            Vector3 horizontalVelocity = GetHorizontalVelocity();
            Vector2 horizontalMovement = new Vector2(horizontalVelocity.x, horizontalVelocity.z);

            return horizontalMovement.magnitude > minMagnitude;
        }

        protected void SetBaseRotationData()
        {
            movementShareData.RotationData = moveData.BaseRotationData;
            movementShareData.TimeToReachTargetRotation = movementShareData.RotationData.TargetRotationReachTime;
        }

        protected void SetBaseCameraRecentering()
        {
            movementShareData.BackwardsCameraRecenteringData = moveData.BackwardsCameraRecenteringData;
            movementShareData.SidewaysCameraRecenteringData = moveData.SidewaysCameraRecenteringData;
        }

        protected void UpdateCameraRecentering(Vector2 moveInput)
        {
            if (moveInput == Vector2.zero)
                return;

            if (moveInput == Vector2.up)
            {
                DisableCameraRecentering();
                return;
            }

            float cameraVerticalAngle = stateMachine.Player.Camera.CameraTransform.eulerAngles.x;

            if (cameraVerticalAngle >= 270.0f)
                cameraVerticalAngle -= 360.0f;

            cameraVerticalAngle = Mathf.Abs(cameraVerticalAngle);
            if (moveInput == Vector2.down)
            {
                SetCameraRecentering(cameraVerticalAngle, movementShareData.BackwardsCameraRecenteringData);
                return;
            }

            SetCameraRecentering(cameraVerticalAngle, movementShareData.SidewaysCameraRecenteringData);
        }

        protected void SetCameraRecentering(float cameraVerticalAngle, List<PlayerCameraRecenteringData> datas)
        {
            foreach (var data in datas)
            {
                if (!data.IsValidRange(cameraVerticalAngle))
                    continue;

                EnableCameraRecentering(data.WaitTime, data.RecenteringTime);
                return;
            }

            DisableCameraRecentering();
        }

        protected void EnableCameraRecentering(float waitTime = -1.0f, float recenteringTime = -1.0f)
        {
            float moveSpeed = GetSpeed();
            if (moveSpeed == 0.0f)
                moveSpeed = moveData.BaseSpeed;

            stateMachine.Player.CameraUtility.EnableRecentering(waitTime, recenteringTime, moveData.BaseSpeed, moveSpeed);
        }

        protected void DisableCameraRecentering()
        {
            stateMachine.Player.CameraUtility.DisableRecentering();
        }

        protected virtual void OnContactWithGround(Collider collider)
        { }

        protected virtual void OnContactWithGroundExit(Collider collider)
        { }

        protected virtual void OnMoveCanceled(InputAction.CallbackContext context)
        {
            DisableCameraRecentering();
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

            var currentHorizontalVelocity = GetHorizontalVelocity();
            var newForce = dir * speed - currentHorizontalVelocity;
            physics.RigidBody.AddForce(newForce, ForceMode.VelocityChange);
        }

        private void Rotate(Vector3 dir)
        {
            UpdateRotationByDirect(dir);
            RotateByDirection();
        }

        private void OnMouseMoveStarted(InputAction.CallbackContext context)
        {
            UpdateCameraRecentering(movementShareData.MovementInput);
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            UpdateCameraRecentering(context.ReadValue<Vector2>());
        }
    }
}
