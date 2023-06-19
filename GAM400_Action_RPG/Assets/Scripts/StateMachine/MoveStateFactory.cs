using System.Collections.Generic;

public enum MoveType
{
    Idle, Walk, Sprint, Falling, Landing, Jump
}

public class MoveStateFactory
{
    private Dictionary<MoveType, MoveState> states = new Dictionary<MoveType, MoveState>();
    private IdleState idle;
    private WalkState walk;
    private SprintState sprint;
    private FallingState falling;
    private LandingState landing;
    private JumpState jump;

    public MoveStateFactory(CharacterData data, PlayerCharacterPhysics physics, PlayerCharacter character)
    {
        idle = new IdleState(data);
        walk = new WalkState(data, physics, character);
        sprint = new SprintState(data, physics, character);
        falling = new FallingState(data);
        landing = new LandingState(data);
        jump = new JumpState(data);

        states.Add(MoveType.Idle, idle);
        states.Add(MoveType.Walk, walk);
        states.Add(MoveType.Sprint, sprint);
        states.Add(MoveType.Falling, falling);
        states.Add(MoveType.Landing, landing);
        states.Add(MoveType.Jump, jump);
    }

    public MoveState GetState(MoveType type)
    {
        return states[type];
    }

}
