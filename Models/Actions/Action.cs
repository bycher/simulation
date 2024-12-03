namespace Simulation.Models.Actions;

public abstract class Action(Map map)
{
    protected readonly Map _map = map;

    public abstract void Execute();
}