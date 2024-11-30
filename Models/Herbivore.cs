using Simulation.Services;

namespace Simulation.Models;

public class Herbivore : Creature
{
    public Herbivore(int speed, int health, Map map, Position currentPosition)
        : base(map, speed, health, currentPosition) { }

    public override void MakeMove()
    {
        var searcher = new ResourceSearcher(_map);
        _pathToResource = searcher.FindResource(_currentPosition, IsResourceFound, this);

        if (_pathToResource.Count == 0)
            return;
            
        if (_pathToResource.Count > 1)
        {
            _map.RemoveEntity(_currentPosition, typeof(Herbivore));
            var steps = Math.Min(Speed, _pathToResource.Count - 1);
            _currentPosition = _pathToResource[steps];
            _map.PlaceEntity(_currentPosition, this);
        }
        else
        {
            _map.RemoveEntity(_currentPosition, typeof(Grass));
        }
    }

    private bool IsResourceFound(Position position)
    {
        var cell = _map.GetCell(position);

        return cell != null && cell.Grass != null &&
            (cell.Herbivore == this || cell.Herbivore == null);
    }
}