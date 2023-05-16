using UnityEngine;

public class IdleState : MoveState
{
    private CharacterData data;

    public IdleState(CharacterData data)
    {
        this.data = data;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Enter Idle");
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exit Idle");
    }

    public override void UpdateMove(Vector2 input, ref Vector3 velocity)
    {
        velocity = Vector3.zero;
    }
}
