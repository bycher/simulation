namespace Simulation.Models;

public class Herbivore(Map map, int speed, int health, Position currentPosition, string name)
    : Creature<Grass>(map, speed, health, currentPosition, name)
{
    protected override bool TryConsumeResource(Position position)
    {
        Console.WriteLine($"Herbivore found the grass at {position} and eat it");
        _map.RemoveEntity(position);
        FindNewPath(position);
        return true;
    }
}