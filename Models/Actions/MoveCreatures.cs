using Simulation.Services.Interfaces;

namespace Simulation.Models.Actions;

public class MoveCreatures(IMapRenderer mapRenderer, ManualResetEvent pauseEvent) : Action
{
    private readonly IMapRenderer _mapRenderer = mapRenderer;
    private readonly ManualResetEvent _pauseEvent = pauseEvent;

    public override void Execute(Map map, CancellationToken cancellationToken)
    {
        foreach (var creature in map.Creatures)
        {
            _pauseEvent.WaitOne();
            if (cancellationToken.IsCancellationRequested)
                return;

            creature.MakeMove(map);
            _mapRenderer.Render(map);
            Thread.Sleep(2000);
        }
    }
}