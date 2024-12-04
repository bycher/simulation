using Simulation.Models.Entities;

namespace Simulation.Models;

public class Map
{
    public int N { get; set; }
    public int M { get; set; }

    public List<Creature> Creatures => _entities.Values
                                                .Where(e => e is Creature)
                                                .Cast<Creature>().ToList();

    private readonly Dictionary<Position, Entity> _entities = [];

    public Map(int n, int m)
    {
        N = n;
        M = m;
        AddPaddings();
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

    private void AddPaddings()
    {
        for (int i = 0; i < N; i++)
        {
            PlaceEntity(new Position(i, -1), new Rock());
            PlaceEntity(new Position(i, M), new Rock());
        }

        for (int i = 0; i < M; i++)
        {
            PlaceEntity(new Position(-1, i), new Rock());
            PlaceEntity(new Position(N, i), new Rock());
        }
    }
}