using UnityEngine;

namespace BattleZZang
{
    public class IdleState : PlayerGroundedState
    {
        private PlayerLookAt lookAt;
        private int rotateHash;
        private int directionHash;

        public IdleState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
            lookAt = stateMachine.Player.LookAt;

            rotateHash = Animator.StringToHash("IsTurning");
            directionHash = Animator.StringToHash("TurningDirection");
        }

        public override void Enter()
        {
            movementShareData.MoveSpeedModifier = 0.0f;
            movementShareData.BackwardsCameraRecenteringData = moveData.IdleData.BackwardsCameraRecenteringData;

            base.Enter();
            StartAnimation(animationData.IdleParameterHash);

            movementShareData.CurrentJumpForce = airborneData.JumpData.StationaryForce;

            ResetVelocity();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(animationData.IdleParameterHash);
        }

        public override void Update()
        {
            base.Update();

            bool isRotation = Mathf.Abs(movementShareData.RotationAngle) >= 90.0f;
            Debug.Log(isRotation);
            animator.SetBool(rotateHash, isRotation);
            animator.SetInteger(directionHash, movementShareData.RotationDirection);

            if (movementShareData.MovementInput == Vector2.zero)
                return;

            OnMove();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!IsMoveHorizontal())
                return;

            ResetVelocity();
        }

        public override void OnAnimatorIK(int layerIndex)
        {
            base.OnAnimatorIK(layerIndex);

            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(lookAt.LookAtTransform.position);
        }
    }
}
