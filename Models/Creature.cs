namespace Simulation.Models;

public abstract class Creature : Entity
{
    protected readonly Map _map;
    protected List<Position> _pathToResource = [];
    protected Position _currentPosition;

    public int Speed { get; set; }
    public int Health { get; set; }

    public Creature(Map map, int speed, int health, Position currentPosition)
    {
        _map = map;
        _currentPosition = currentPosition;
        Speed = speed;
        Health = health;
    }
    
    public abstract void MakeMove();
}