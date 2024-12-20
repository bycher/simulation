namespace Simulation.Models.Actions;

public abstract class Action
{
    public abstract void Execute(Map map, CancellationToken cancellationToken);
}