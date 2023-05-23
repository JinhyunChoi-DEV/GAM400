using UnityEngine;

public class WalkState : MoveState
{
    private CharacterData data;

    //TODO Change to Other things
    private PlayerCharacterPhysics physics;

    public WalkState(CharacterData data, PlayerCharacterPhysics physics)
    {
        this.data = data;
        this.physics = physics;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Enter Walk");
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exit Walk");
    }

    public override void UpdateMove(Vector2 input, ref Vector3 velocity)
    {
        var direction = physics.GetDirection();
        var forward = direction.Forward;
        var right = direction.Right;

        velocity = (forward * input.y + right * input.x) * data.WalkSpeed;
    }
}