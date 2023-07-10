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
            movementShareData.CurrentJumpForce = airborneData.JumpData.StationaryForce;

            ResetVelocity();
        }

        public override void Update()
        {
            base.Update();

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
