
public interface IState<T> where T : IState<T>
{
    void Enter();

    void Update();

    void Exit();
}

public interface IStateMachine
{
    void Initialize();
    void Update();
    void FixedUpdate();
    void Clear();

}
