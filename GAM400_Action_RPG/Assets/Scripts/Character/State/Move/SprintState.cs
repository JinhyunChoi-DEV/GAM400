using UnityEngine;

public class SprintState : MoveState
{
    private CharacterData data;
    private PlayerCharacterPhysics physics;
    private PlayerCharacter character;

    public SprintState(CharacterData data, PlayerCharacterPhysics physics, PlayerCharacter character)
    {
        this.data = data;
        this.physics = physics;
        this.character = character;
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
        var direction = character.Look;
        var forward = direction.forward;
        var right = direction.right;

        velocity = (forward * input.y + right * input.x) * data.SprintSpeed;
    }
}