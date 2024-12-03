using Simulation.Services;

namespace Simulation.Models.Entities;

public class Predator(int speed, int health, int attack, Position currentPosition,
                    IResourceSearcher resourceSearcher)
    : Creature<Herbivore>(speed, health, currentPosition, resourceSearcher)
{
    public int Attack { get; set; } = attack;

    protected override bool TryConsumeResource(Map map, Position position)
    {
        map.TryGetEntity(position, out var entity);

        var herbivore = (Herbivore)entity!;
        herbivore.Health -= Attack;
        if (herbivore.Health <= 0)
        {
            Console.WriteLine($"Predator killed the herbivore at {position}");
            map.RemoveEntity(position);
            FindNewPath(position);
        }
        else
        {
            Console.WriteLine($"Predator at {_currentPosition} attacked the herbivore at {position}," +
                            $" remaining health: {herbivore.Health}");
            _stepsInPath--;
        }

        return herbivore.Health <= 0;
    }
}