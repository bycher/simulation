using Simulation.Models;
using Simulation.Models.Entities;

namespace Simulation.Services;

public class ResourceSearcher<TResource>(Map map) : IResourceSearcher where TResource : Entity
{
    private readonly Map _map = map;

    private List<Position> _visitedPositions = [];
    private Dictionary<Position, Position> _transitions = [];
    private Queue<Position> _queue = new();

    public List<Position> FindResource(Position start)
    {
        Reset();
        _queue.Enqueue(start);
        _visitedPositions.Add(start);

        while (_queue.Count > 0)
        {
            var position = _queue.Dequeue();
            if (_map.CheckForEntityType<TResource>(position))
                return ConstructPath(start, position);

            ProcessAdjacentPositions(position);
        }

        return [];
    }

    public void Reset()
    {
        _queue = [];
        _visitedPositions = [];
        _transitions = [];
    }

    private void ProcessAdjacentPositions(Position position)
    {
        foreach (var adjacent in position.Adjacents)
        {
            if (!_visitedPositions.Contains(adjacent) &&
                (_map.IsPositionFree(adjacent) || _map.CheckForEntityType<TResource>(adjacent)))
            {
                _visitedPositions.Add(adjacent);
                _transitions[adjacent] = position;
                _queue.Enqueue(adjacent);
            }
        }
    }

    private List<Position> ConstructPath(Position start, Position finish)
    {
        List<Position> path = [];
        var position = finish;

        while (position != start)
        {
            path.Add(position);
            position = _transitions[position];
        }
        path.Reverse();

        return path;
    }
}