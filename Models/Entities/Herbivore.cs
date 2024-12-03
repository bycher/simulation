using Simulation.Services;

namespace Simulation.Models.Entities;

public class Herbivore(int speed, int health, Position currentPosition,
                    IResourceSearcher resourceSearcher)
    : Creature<Grass>(speed, health, currentPosition, resourceSearcher)
{
    protected override bool TryConsumeResource(Map map, Position position)
    {
        Console.WriteLine($"Herbivore found the grass at {position} and eat it");
        map.RemoveEntity(position);
        FindNewPath(position);
        return true;
    }
}