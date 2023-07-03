using UnityEngine;

namespace BattleZZang
{
    public class IdleState : PlayerGroundedState
    {
        public IdleState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        { }

        public override void Enter()
        {
            base.Enter();

            movementShareData.MoveSpeedModifier = 0.0f;
            physics.ResetVelocity();
        }

        public override void Update()
        {
            base.Update();

            if (movementShareData.MovementInput == Vector2.zero)
                return;

            OnMove();
        }
    }
}
