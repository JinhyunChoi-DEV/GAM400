using System.Collections.Generic;

public class StateMachine
{
    private IState currentState;
    private IState prevState;
    private List<IState> states;

    //TODO: Do I need this functions?
    public void Add(IState state)
    { }

    //TODO: Do I need this functions?
    public void Remove(IState state)
    { }


    public void ChangeState(IState state)
    {
        prevState = currentState;
        currentState = state;
        currentState.Initialize();
    }

    public void Update()
    {
        currentState.Update();
    }
}
