namespace BattleZZang
{
    public class HardStopState : PlayerStopState
    {
        public HardStopState(PlayerMoveStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            movementShareData.MoveDecelerationForce = moveData.StopData.HardDecelerationForce;
            movementShareData.CurrentJumpForce = airborneData.JumpData.StrongForce;
        }

        protected override void OnMove()
        {
            if (stateMachine.MovementShareData.IsWalk)
                return;

            stateMachine.Change(stateMachine.Run);
        }
    }
}
