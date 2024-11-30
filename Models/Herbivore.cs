using Simulation.Services;

namespace Simulation.Models;

public class Herbivore : Creature
{
    private List<Position> _pathToResource = [];
    private Position _currentPosition;

    public Herbivore(int speed, int health, Map map, Position currentPosition)
        : base(map, speed, health)
    {
        _currentPosition = currentPosition;
    }

    public override void MakeMove()
    {
        var searcher = new ResourceSearcher(_map, this, IsResourceFound);
        _pathToResource =  searcher.FindResource(_currentPosition);

        if (_pathToResource.Count > 0)
            _map.RemoveEntity(_currentPosition);
    
        if (_pathToResource.Count > 1)
        {
            var steps = Math.Min(Speed, _pathToResource.Count - 1);
            _currentPosition = _pathToResource[steps];
            _map.PlaceEntity(_currentPosition, this);
        }
    }

    private bool IsResourceFound(Position position)
    {
        var entitiesAtPosition = _map.GetEntitiesAtPosition(position);

        return entitiesAtPosition != null &&
            (entitiesAtPosition.Count == 1 && entitiesAtPosition[0] is Grass ||
             entitiesAtPosition.Count == 2 && entitiesAtPosition[0] is Grass && entitiesAtPosition[1] == this);
    }
}