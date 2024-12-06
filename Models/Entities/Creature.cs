using Serilog;
using Simulation.Models.Options;
using Simulation.Services.Interfaces;

namespace Simulation.Models.Entities;

public abstract class Creature(CreatureOptions options, Position currentPosition) : Entity(options)
{
    protected Position _currentPosition = currentPosition;
    protected Queue<Position> _path = [];

    public int Speed { get; set; } = options.Speed;
    public int Health { get; set; } = options.Health;

    public abstract void MakeMove(Map map);
}

public abstract class Creature<TResource>(CreatureOptions options, Position currentPosition,
                                          IResourceSearcher resourceSearcher, ILogger logger)
    : Creature(options, currentPosition) where TResource : Entity
{
    protected readonly ILogger _logger = logger;
    protected readonly IResourceSearcher _resourceSearcher = resourceSearcher;

    protected abstract bool TryConsumeResource(Map map, Position position);

    public override void MakeMove(Map map)
    {
        _path = _resourceSearcher.FindResource(_currentPosition);
        var initialPosition = _currentPosition;
        for (int _ = 0; _ < Speed; _++)
        {
            if (!_path.TryDequeue(out var nextPosition))
                break;

            if (map.CheckForEntityType<TResource>(nextPosition))
            {
                if (TryConsumeResource(map, nextPosition))
                    _path = _resourceSearcher.FindResource(nextPosition);
                else
                    continue;
            }
            ChangePosition(map, nextPosition);
        }
        _logger.Information($"{GetType().Name} made step from {initialPosition} to {_currentPosition}");
    }

    private void ChangePosition(Map map, Position newPosition)
    {
        map.RemoveEntity(_currentPosition);
        map.PlaceEntity(newPosition, this);

        _logger.Debug($"{GetType().Name} made step from {_currentPosition} to {newPosition}");
        _currentPosition = newPosition;
    }
}