namespace BattleZZang
{
    public class PlayerMoveStateMachine : StateMachine
    {
        public Player Player { get; }
        public IdleState Idle { get; }
        public WalkState Walk { get; }
        public RunState Run { get; }
        public SprintState Sprint{ get; }

        public PlayerMoveStateMachine(Player player, CharacterMoveData moveData)
        {
            Player = player;

            Idle = new IdleState(this, moveData);
            Walk = new WalkState(this, moveData);
            Run = new RunState(this, moveData); 
            Sprint = new SprintState(this, moveData);
        }
    }
}
