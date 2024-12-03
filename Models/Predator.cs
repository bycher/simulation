namespace Simulation.Models;

public class Predator(Map map, int speed, int health, int attack, Position currentPosition, string name)
    : Creature<Herbivore>(map, speed, health, currentPosition, name)
{
    public int Attack { get; set; } = attack;

    protected override bool TryConsumeResource(Position position)
    {
        _map.TryGetEntity(position, out var entity);
        
        var herbivore = (Herbivore)entity!;
        herbivore.Health -= Attack;
        if (herbivore.Health <= 0)
        {
            Console.WriteLine($"Predator killed the herbivore at {position}");
            _map.RemoveEntity(position);
            FindNewPath(position);
            return true;
        }
        else
        {
            Console.WriteLine($"Predator at {_currentPosition} attacked the herbivore at {position}," +
                            $" remaining health: {herbivore.Health}");
            _stepsInPath--;
        }

        return false;
    }
}