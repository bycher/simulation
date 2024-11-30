namespace Simulation.Models;

public abstract class Creature : Entity
{
    protected readonly Map _map;

    public int Speed { get; set; }
    public int Health { get; set; }

    public Creature(Map map, int speed, int health)
    {
        _map = map;
        Speed = speed;
        Health = health;
    }
    
    public abstract void MakeMove();
}