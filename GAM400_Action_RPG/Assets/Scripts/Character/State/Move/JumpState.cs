using UnityEngine;

public class JumpState : MoveState
{
    private CharacterData data;

    public JumpState(CharacterData data)
    {
        this.data = data;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Enter Jump");
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exit Jump");
    }

    public override void UpdateMove(Vector2 input, ref Vector3 velocity)
    {
        velocity.y = data.JumpPower;
    }
}