using UnityEngine;

public class CharacterMoveStateMachine
{
    private MoveStateFactory factory;
    private PlayerCharacterPhysics physics;
    private PlayerCharacter character;

    private InputState input;
    private MoveType current;
    private MoveType prev;

    public CharacterMoveStateMachine(CharacterData data, PlayerCharacterPhysics physics, PlayerCharacter character)
    {
        this.physics = physics;
        factory = new MoveStateFactory(data, physics, character);

        current = MoveType.Idle;
    }

    public void Update()
    {
        input = InputCommand.GetInput();
        prev = current;
        UpdateState();

        var prevState = factory.GetState(prev);
        var currentState = factory.GetState(current);
        if (prev == current)
        {
            currentState.Update();
            return;
        }

        prevState.Exit();

        currentState.Enter();
    }

    public void FixedUpdate()
    {
        Vector3 velocity = physics.Velocity;
        
        var currentState = factory.GetState(current);
        currentState.UpdateMove(input.Direction, ref velocity);
        physics.ApplyVelocity(velocity);
    }

    private void UpdateState()
    {
        if (!physics.IsGround)
        {
            current = MoveType.Falling;
            return;
        }

        if (prev == MoveType.Falling && prev != MoveType.Landing)
            current = MoveType.Landing;

        // check one frame more
        else if (input.Jump || prev == MoveType.Jump)
            current = MoveType.Jump;
        
        else if (input.Direction.magnitude != 0 && input.Sprint)
            current = MoveType.Sprint;

        else if (input.Direction.magnitude != 0)
            current = MoveType.Walk;

        else
            current = MoveType.Idle;
    }
}
