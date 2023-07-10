namespace BattleZZang
{
    public class PlayerMoveStateMachine : StateMachine
    {
        public Player Player { get; }
        public PlayerMovementShareData MovementShareData { get; }

        public IdleState Idle { get; }
        public WalkState Walk { get; }
        public RunState Run { get; }
        public DashState Dash { get; }
        public SprintState Sprint{ get; }

        public LightStopState LightStop { get; }
        public MediumStopState MediumStop { get; }
        public HardStopState HardStop { get; }

        public JumpState Jump { get; }
        public FallState Fall { get; }

        public LightLandState LightLand { get; }
        public RollState Roll { get; }
        public HardLandState HardLand { get; }

        public PlayerMoveStateMachine(Player player)
        {
            Player = player;
            MovementShareData = new PlayerMovementShareData();

            Idle = new IdleState(this);
            Walk = new WalkState(this);
            Run = new RunState(this); 
            Dash = new DashState(this);
            Sprint = new SprintState(this);

            LightStop = new LightStopState(this);
            MediumStop = new MediumStopState(this);
            HardStop = new HardStopState(this);

            Jump = new JumpState(this);
            Fall = new FallState(this);

            LightLand = new LightLandState(this);
            Roll = new RollState(this);
            HardLand = new HardLandState(this);
        }
    }
}
