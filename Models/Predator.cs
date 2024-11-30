using Simulation.Services;

namespace Simulation.Models;

public class Predator : Creature
{
    public int Attack { get; set; }

    public Predator(Map map, int speed, int health, Position currentPosition, int attack)
        : base(map, speed, health, currentPosition)
    {
        Attack = attack;
    }

    public override void MakeMove()
    {
        var searcher = new ResourceSearcher(_map);
        _pathToResource = searcher.FindResource(_currentPosition, IsResourceFound, this);

        if (_pathToResource.Count == 0)
            return;

        _map.RemoveEntity(_currentPosition, typeof(Predator));
        var steps = Math.Min(Speed, _pathToResource.Count - 1);
        _currentPosition = _pathToResource[steps];
        _map.PlaceEntity(_currentPosition, this);

        if (Speed >= _pathToResource.Count - 1)
        {
            var cell = _map.GetCell(_currentPosition);
            cell!.Herbivore!.Health -= Attack;
            if (cell!.Herbivore!.Health <= 0)
                _map.RemoveEntity(_currentPosition, typeof(Herbivore));
        }
    }

    private bool IsResourceFound(Position position)
    {
        var cell = _map.GetCell(position);

        return cell != null && cell.Herbivore != null && cell.Predator == null;
    }
}