using UnityEngine;

namespace BattleZZang
{
    public class IdleState : PlayerGroundedState
    {
        private int idleTurningDirectionHash;
        private int doingRotateHash;
        private int needRotateHash;

        private bool doingRotate;
        private float rotateAngle;
        private int turnDirection;

        public IdleState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
            idleTurningDirectionHash = Animator.StringToHash("IdleTurningDirection");
            doingRotateHash = Animator.StringToHash("DoingRotate");
            needRotateHash = Animator.StringToHash("NeedRotate");
        }

        public override void Enter()
        {
            movementShareData.MoveSpeedModifier = 0.0f;
            movementShareData.BackwardsCameraRecenteringData = moveData.IdleData.BackwardsCameraRecenteringData;

            base.Enter();
            StartAnimation(animationData.IdleParameterHash);

            movementShareData.CurrentJumpForce = airborneData.JumpData.StationaryForce;
            doingRotate = false;

            ResetVelocity();
        }

        public override void Exit()
        {
            base.Exit();

            doingRotate = false;
            StopAnimation(animationData.IdleParameterHash);
        }

        public override void Update()
        {
            UpdateIdleRotate();

            base.Update();

            animator.SetInteger(idleTurningDirectionHash, turnDirection);
            if (!doingRotate)
            {
                bool needRotate = Mathf.Abs(rotateAngle) > 90.0f;
                animator.SetBool(needRotateHash, needRotate);
            }

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
            doingRotate = true;
        }

        public override void OnAnimationExit()
        {
            doingRotate = false;
        }

        private void UpdateIdleRotate()
        {
            Vector3 cameraRotation = new Vector3(WrapAngle(camera.CameraTransform.eulerAngles.x),
                WrapAngle(camera.CameraTransform.eulerAngles.y),
                WrapAngle(camera.CameraTransform.eulerAngles.z));

            Vector3 playerRotation = new Vector3(WrapAngle(stateMachine.Player.transform.eulerAngles.x),
                WrapAngle(stateMachine.Player.transform.eulerAngles.y),
                WrapAngle(stateMachine.Player.transform.eulerAngles.z));

            Vector3 diffPlayerToCamera = cameraRotation - playerRotation;
            rotateAngle = WrapAngle(diffPlayerToCamera.y);

            if (rotateAngle > 0)
                turnDirection= 1;
            else if (rotateAngle < 0)
                turnDirection = -1;
            else if (rotateAngle == 0)
                turnDirection = 0;

            lookAt.Update(rotateAngle);
        }

        private float WrapAngle(float angle)
        {
            angle %= 360.0f;

            if (angle > 180.0f)
                return angle - 360.0f;

            if (angle < -180.0f)
                return angle + 360.0f;

            return angle;
        }
    }
}
