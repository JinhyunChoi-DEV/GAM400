using UnityEngine;

namespace BattleZZang
{
    public class IdleState : PlayerGroundedState
    {
        private PlayerLookAt lookAt;
        private int rotateHash;
        private int directionHash;
        private int doingRotationHash;

        private bool doingRotation;

        public IdleState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
            lookAt = stateMachine.Player.LookAt;

            rotateHash = Animator.StringToHash("IsTurning");
            directionHash = Animator.StringToHash("TurningDirection");
            doingRotationHash = Animator.StringToHash("DoingRotation");
        }

        public override void Enter()
        {
            movementShareData.MoveSpeedModifier = 0.0f;
            movementShareData.BackwardsCameraRecenteringData = moveData.IdleData.BackwardsCameraRecenteringData;
            doingRotation = false;

            base.Enter();
            StartAnimation(animationData.IdleParameterHash);

            movementShareData.CurrentJumpForce = airborneData.JumpData.StationaryForce;

            ResetVelocity();
        }

        public override void Exit()
        {
            base.Exit();

            doingRotation = false;
            StopAnimation(animationData.IdleParameterHash);
        }

        public override void Update()
        {
            base.Update();


            animator.SetBool(doingRotationHash, doingRotation);
            animator.SetInteger(directionHash, movementShareData.RotationDirection);

            bool isRotation = Mathf.Abs(movementShareData.RotationAngle) >= 90.0f;
            animator.SetBool(rotateHash, isRotation);
            
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

        public override void OnAnimationEnter()
        {
            doingRotation = true;
        }

        public override void OnAnimationExit()
        {
            doingRotation = false;
        }
    }
}
