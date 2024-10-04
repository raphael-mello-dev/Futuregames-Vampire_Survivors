public class StateMachine
{
    public IState currentState {  get; private set; }

    public StateMachine()
    { }

    public void TransitionTo<T>() where T : IState, new()
    {
        currentState?.OnEnd();
        currentState = new T();
        currentState.OnStart();
    }

    public void OnTick()
    {
        currentState?.OnTick();
    }
}