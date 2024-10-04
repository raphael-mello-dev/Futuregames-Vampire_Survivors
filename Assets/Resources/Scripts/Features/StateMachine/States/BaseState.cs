public interface IState
{
    void OnStart();

    void OnTick();
    
    void OnEnd();
}

public abstract class BaseState : IState
{

    public virtual void OnStart()
    {

    }

    public virtual void OnTick()
    {

    }
    public virtual void OnEnd()
    {

    }
}