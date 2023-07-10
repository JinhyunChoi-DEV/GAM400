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

            movementShareData.MoveDecelerationForce = moveData.StopData.MediumDecelerationForce;
            movementShareData.CurrentJumpForce = airborneData.JumpData.MediumForce;
        }
    }
}
