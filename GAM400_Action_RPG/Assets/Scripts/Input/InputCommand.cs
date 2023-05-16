using UnityEngine;

public struct MoveButtons
{
    public bool Jump { get; private set; }
    public bool Sprint { get; private set; }
    public bool Dodge { get; private set; }

    public MoveButtons(bool jump, bool sprint, bool dodge)
    {
        Jump = jump;
        Sprint = sprint;
        Dodge = dodge;
    }
}

public struct InputState
{
    public Vector2 Direction { get; private set; }
    public bool Jump { get; private set; }
    public bool Sprint { get; private set; }
    public bool Dodge { get; private set; }
    //public bool Idle { get; private set; }

    public InputState(float horizontal, float vertical, MoveButtons move)
    {
        Direction = new Vector2(horizontal, vertical);
        Jump = move.Jump;
        Sprint = move.Sprint;
        Dodge = move.Dodge;
        //Idle = !Jump && !Sprint && !Dodge && (Direction.magnitude == 0);
    }
}

public static class InputCommand
{
    public static InputState GetInput()
    {
        return new InputState(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"),
            GetMove()
        );
    }

    // Jump - F
    // Sprint - Left Shift
    // Dodge - Space
    private static MoveButtons GetMove()
    {
        return new MoveButtons(
            Input.GetButton("Jump"),
            Input.GetButton("Sprint"),
            Input.GetButton("Dodge")
        );
    }
}
