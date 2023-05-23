using UnityEngine;

public class LandingState : MoveState
{
    private CharacterData data;

    public LandingState(CharacterData data)
    {
        this.data = data;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Enter Landing");
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exit Landing");
    }

    public override void UpdateMove(Vector2 input, ref Vector3 velocity)
    {
        velocity = Vector3.zero;
    }
}