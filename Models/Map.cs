using Simulation.Models.Entities;

namespace Simulation.Models;

public class Map
{
    public int Rows { get; init; }
    public int Columns { get; init; }

    public List<Creature> Creatures => GetEntities<Creature>();

    private readonly Dictionary<Position, Entity> _entities = [];

    public Map(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
    }

    public List<T> GetEntities<T>()
    {
        return _entities.Values.Where(e => e is T).Cast<T>().ToList();
    }

    public bool IsPositionFree(Position position)
    {
        return !_entities.ContainsKey(position);
    }

    public void PlaceEntity(Position position, Entity entity)
    {
        _entities[position] = entity;
    }

    public bool TryGetEntity(Position position, out Entity? entity)
    {
        return _entities.TryGetValue(position, out entity);
    }

    public bool TryGetEntity(int x, int y, out Entity? entity)
    {
        return TryGetEntity(new Position(x, y), out entity);
    }

    public bool RemoveEntity(Position position)
    {
        return _entities.Remove(position);
    }

    public bool CheckForEntityType<T>(Position position)
    {
        TryGetEntity(position, out var entity);
        return entity is T;
    }
}