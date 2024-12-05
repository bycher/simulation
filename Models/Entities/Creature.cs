using Serilog;
using Simulation.Models.Options;
using Simulation.Services;

namespace Simulation.Models.Entities;

public abstract class Creature(CreatureOptions options, Position currentPosition) : Entity(options)
{
    protected Position _currentPosition = currentPosition;
    protected List<Position> _path = [];
    protected int _stepsInPath;

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
        FindNewPath(_currentPosition);

        for (var _ = 0; _ < Speed && _path.Count > 0; _++)
        {
            var nextPosition = _path[_stepsInPath++];
            if (_stepsInPath == _path.Count)
            {
                if (TryConsumeResource(map, nextPosition))
                {
                    ChangePosition(map, nextPosition);
                }
            }
            else
            {
                ChangePosition(map, nextPosition);
            }
        }
    }

    private void ChangePosition(Map map, Position newPosition)
    {
        map.RemoveEntity(_currentPosition);
        map.PlaceEntity(newPosition, this);

        _logger.Information($"{GetType().Name} made step from {_currentPosition} to {newPosition}");
        _currentPosition = newPosition;
    }

    protected void FindNewPath(Position position)
    {
        _resourceSearcher.Reset();
        _path = _resourceSearcher.FindResource(position);
        _stepsInPath = 0;
    }
}