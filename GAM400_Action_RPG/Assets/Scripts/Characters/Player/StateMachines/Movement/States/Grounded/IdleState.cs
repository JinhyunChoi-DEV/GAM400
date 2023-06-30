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

            reusableData.MoveSpeedModifier = 0.0f;
            physics.ResetVelocity();
        }

        public override void Update()
        {
            base.Update();

            if (reusableData.MovementInput == Vector2.zero)
                return;

            OnMove();
        }
    }
}
