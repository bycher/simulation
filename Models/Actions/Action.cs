namespace Simulation.Models.Actions;

public abstract class Action
{
    protected readonly Map _map;

    public Action(Map map)
    {
        _map = map;
    }

    public abstract void Execute();
}