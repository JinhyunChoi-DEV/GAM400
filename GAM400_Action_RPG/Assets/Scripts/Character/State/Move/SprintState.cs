using UnityEngine;

public class SprintState : MoveState
{
    private CharacterData data;
    private PlayerCharacterPhysics physics;

    public SprintState(CharacterData data, PlayerCharacterPhysics physics)
    {
        this.data = data;
        this.physics = physics;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Enter Sprint");
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exit Sprint");
    }

    public override void UpdateMove(Vector2 input, ref Vector3 velocity)
    {
        var direction = physics.GetDirection();
        var forward = direction.Forward;
        var right = direction.Right;

        velocity = (forward * input.y + right * input.x) * data.SprintSpeed;
    }
}