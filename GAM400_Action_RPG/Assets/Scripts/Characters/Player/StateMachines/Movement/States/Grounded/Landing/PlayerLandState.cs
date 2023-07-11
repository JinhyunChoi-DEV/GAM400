namespace BattleZZang
{
    public class PlayerLandState : PlayerGroundedState
    {
        public PlayerLandState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        { }

        public override void Enter()
        {
            base.Enter();
            StartAnimation(animationData.LandParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(animationData.LandParameterHash);
        }
    }
}
