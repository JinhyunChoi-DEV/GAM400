using UnityEngine;

namespace BattleZZang
{
    public class LightLandState : PlayerLandState
    {
        public LightLandState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            movementShareData.MoveSpeedModifier = 0.0f;

            base.Enter();

            stateMachine.MovementShareData.CurrentJumpForce = airborneData.JumpData.StationaryForce;

            ResetVelocity();
        }

        public override void Update()
        {
            base.Update();

            if (stateMachine.MovementShareData.MovementInput == Vector2.zero)
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

        public override void OnAnimationTransition()
        {
            stateMachine.Change(stateMachine.Idle);
        }
    }
}
