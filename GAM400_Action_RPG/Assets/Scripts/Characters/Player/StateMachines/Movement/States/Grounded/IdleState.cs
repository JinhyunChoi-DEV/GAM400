using UnityEngine;

namespace BattleZZang
{
    public class IdleState : PlayerGroundedState
    {
        public IdleState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
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

            //animator.SetInteger(animationData.IdleTurningDirectionHash, movementShareData.RotationDirection);

            ////TODO: doingRotation 넣어야함
            //bool needRotate = Mathf.Abs(movementShareData.RotationAngle) > 90.0f;
            //animator.SetBool(animationData.NeedRotateHash, needRotate);

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
    }
}
