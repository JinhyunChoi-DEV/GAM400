namespace BattleZZang
{
    public class PlayerMoveStateMachine : StateMachine
    {
        public Player Player { get; }
        public PlayerReusableData ReusableData { get; }

        public IdleState Idle { get; }
        public WalkState Walk { get; }
        public RunState Run { get; }
        public SprintState Sprint{ get; }

        public PlayerMoveStateMachine(Player player)
        {
            Player = player;
            ReusableData = new PlayerReusableData();

            Idle = new IdleState(this);
            Walk = new WalkState(this);
            Run = new RunState(this); 
            Sprint = new SprintState(this);
        }
    }
}
