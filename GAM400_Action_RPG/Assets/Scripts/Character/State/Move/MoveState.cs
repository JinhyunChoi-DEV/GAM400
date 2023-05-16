using UnityEngine;

public abstract class MoveState : IState<MoveState>
{

    public virtual void Enter()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void Exit()
    {

    }

    public abstract void UpdateMove(Vector2 input, ref Vector3 velocity);
}
