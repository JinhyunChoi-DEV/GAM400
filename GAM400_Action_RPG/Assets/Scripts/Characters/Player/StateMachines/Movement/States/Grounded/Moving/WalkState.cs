using UnityEngine.InputSystem;

namespace BattleZZang
{
    public class WalkState : PlayerMovingState
    {
        public WalkState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        { }

        public override void Enter()
        {
            base.Enter();

            reusableData.MoveSpeedModifier = moveData.WalkData.SpeedModifier;
        }

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.Change(stateMachine.Run);
        }
    }
}
