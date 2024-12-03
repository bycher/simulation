using Simulation.Services;

namespace Simulation.Models;

public abstract class Creature(Map map, int speed, int health, Position currentPosition, string name) : Entity
{
    protected readonly Map _map = map;
    protected Position _currentPosition = currentPosition;
    protected List<Position> _path = [];
    protected int _stepsInPath;
    protected string _name = name;

    public int Speed { get; set; } = speed;
    public int Health { get; set; } = health;

    public abstract void MakeMove();
}

public abstract class Creature<TResource>(Map map, int speed, int health,
                                          Position currentPosition, string name)
    : Creature(map, speed, health, currentPosition, name)
    where TResource : Entity
{
    protected ResourceSearcher<TResource> _resourceSearcher = new(map);

    protected abstract bool TryConsumeResource(Position resourcePosition);

    public override void MakeMove()
    {
        FindNewPath(_currentPosition);
        
        for (var _ = 0; _ < Speed && _path.Count > 0; _++)
        {
            var nextPosition = _path[_stepsInPath++];
            if (_stepsInPath == _path.Count)
            {
                var resourceConsumed = TryConsumeResource(nextPosition);
                if (resourceConsumed)
                    ChangePosition(nextPosition);
            }
            else
                ChangePosition(nextPosition);
        }
    }

    public void ChangePosition(Position newPosition)
    {
        _map.RemoveEntity(_currentPosition);
        _map.PlaceEntity(newPosition, this);
        Console.WriteLine($"{_name} made step from {_currentPosition} to {newPosition}");
        _currentPosition = newPosition;
    }

    protected void FindNewPath(Position position)
    {
        _resourceSearcher.Reset();
        _path = _resourceSearcher.FindResource(position);
        _stepsInPath = 0;
    }
}