using Simulation.Models;

namespace Simulation.Services;

public class ResourceSearcher
{
    private readonly Map _map;
    private readonly Creature _creature;
    private readonly Predicate<Position> _resourceFoundCondition;

    public ResourceSearcher(
        Map map, Creature creature, Predicate<Position> resourceFoundCondition)
    {
        _creature = creature;
        _map = map;
        _resourceFoundCondition = resourceFoundCondition;
    }

    public List<Position> FindResource(Position start)
    {
        var visited = new List<Position>();
        var transitions = new Dictionary<Position, Position>();

        var queue = new Queue<Position>();
        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            var position = queue.Dequeue();
            if (_resourceFoundCondition(position))
                return ConstructPath(start, position, transitions);

            foreach (var adjacent in position.Adjacents)
                if (!visited.Contains(adjacent) && _map.CanBePassedThrough(adjacent))
                {
                    visited.Add(adjacent);
                    transitions[adjacent] = position;
                    queue.Enqueue(adjacent);
                }
        }

        return [];
    } 

    private static List<Position> ConstructPath(
        Position start, Position finish, Dictionary<Position, Position> transitions)
    {
        List<Position> path = [finish];
        var position = finish;

        while (position != start)
        {
            position = transitions[position];
            path.Add(position);
        }
        path.Reverse();

        return path;
    }
}