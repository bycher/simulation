using Simulation.Models;

namespace Simulation.Services;

public class ResourceSearcher(Map map)
{
    private readonly Map _map = map;

    public List<Position> FindResource(Position start, Predicate<Position> resourceFoundCondition, Entity subject)
    {
        var visited = new List<Position>();
        var transitions = new Dictionary<Position, Position>();

        var queue = new Queue<Position>();
        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            var position = queue.Dequeue();
            if (resourceFoundCondition(position))
                return ConstructPath(start, position, transitions);

            foreach (var adjacent in position.Adjacents)
                if (!visited.Contains(adjacent) && _map.CanBePassedThrough(adjacent, subject))
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