using UnityEngine;

public class FallingState : MoveState
{
    private CharacterData data;

    private float currentMultiplier;
    private float fallingTimer;

    public FallingState(CharacterData data)
    {
        this.data = data;
        currentMultiplier = 1.0f;
        fallingTimer = data.GravityTimer;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Enter Falling");
        currentMultiplier = 1.0f;
        fallingTimer = data.GravityTimer;
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exit Falling");
        currentMultiplier = 1.0f;
        fallingTimer = data.GravityTimer;
    }

    public override void UpdateMove(Vector2 input, ref Vector3 velocity)
    {
        fallingTimer -= Time.fixedDeltaTime;

        if (fallingTimer < 0.0f)
            currentMultiplier += data.GravityMultiplier;

        float gravity = data.Gravity * Time.fixedDeltaTime * currentMultiplier;
        velocity.y -= gravity;
    }
}