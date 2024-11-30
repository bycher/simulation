using Simulation.Services;

namespace Simulation.Models.Actions;

public class MoveCreatures : Action
{
    private readonly MapRenderer _mapRenderer;

    public MoveCreatures(Map map, MapRenderer mapRenderer) : base(map)
    {
        _mapRenderer = mapRenderer;
    }

    public override void Execute()
    {
        foreach (var creature in _map.Creatures)
        {
            creature.MakeMove();
            Thread.Sleep(500);
            _mapRenderer.Render();
        }
    }
}