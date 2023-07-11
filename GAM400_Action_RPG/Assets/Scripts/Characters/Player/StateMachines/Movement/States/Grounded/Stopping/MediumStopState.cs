namespace BattleZZang
{
    public class MediumStopState : PlayerStopState
    {
        public MediumStopState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation(animationData.MediumStopParameterHash);

            movementShareData.MoveDecelerationForce = moveData.StopData.MediumDecelerationForce;
            movementShareData.CurrentJumpForce = airborneData.JumpData.MediumForce;
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(animationData.MediumStopParameterHash);
        }
    }
}
