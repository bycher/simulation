using Simulation.Services;

namespace Simulation.Models.Actions;

public class MoveCreatures(Map map, MapRenderer mapRenderer, ManualResetEvent pauseEvent) : Action(map)
{
    private readonly MapRenderer _mapRenderer = mapRenderer;
    private readonly ManualResetEvent _pauseEvent = pauseEvent;

    public override void Execute()
    {
        foreach (var creature in _map.Creatures)
        {
            _pauseEvent!.WaitOne();
            _mapRenderer.Render();
            creature.MakeMove();
            Thread.Sleep(2000);
        }
    }
}