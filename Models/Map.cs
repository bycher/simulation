namespace Simulation.Models;

public class Map
{
    public int N { get; set; }
    public int M { get; set; }
    
    public List<Creature> Creatures => _entities.Values
                                                .SelectMany(e => e)
                                                .Where(e => e is Creature)
                                                .Cast<Creature>()
                                                .ToList();

    private readonly Dictionary<Position, List<Entity>> _entities = [];

    public Map(int n, int m)
    {
        N = n;
        M = m;

        AddPaddings();
    }

    public bool IsPositionFree(Position coordinates)
    {
        return !_entities.ContainsKey(coordinates);
    }

    public void PlaceEntity(Position position, Entity entity)
    {
        var entitiesAtPosition = GetEntitiesAtPosition(position);

        if (entitiesAtPosition is null)
            _entities[position] = [entity];
        else
            entitiesAtPosition.Add(entity);
    }

    public void PlaceEntity(int x, int y, Entity entity)
    {
        PlaceEntity(new Position(x, y), entity);
    }

    public List<Entity>? GetEntitiesAtPosition(int x, int y)
    {
        return GetEntitiesAtPosition(new Position(x, y));
    }

    public List<Entity>? GetEntitiesAtPosition(Position position)
    {
        _entities.TryGetValue(position, out var value);
        return value;
    }

    public void RemoveEntity(Position position)
    {
        var entitiesAtPosition = GetEntitiesAtPosition(position) ??
            throw new NullReferenceException("No entities at position");

        if (entitiesAtPosition.Count == 1)
            _entities.Remove(position);
        else
            entitiesAtPosition.RemoveAt(0);
    }

    public bool CanBePassedThrough(Position position)
    {
        var entitiesAtPosition = GetEntitiesAtPosition(position);

        return entitiesAtPosition is null ||
            entitiesAtPosition.Count == 1 && entitiesAtPosition[0] is Grass;
    }

    private void AddPaddings()
    {
        for (int i = 0; i < N; i++)
        {
            PlaceEntity(i, -1, new Rock());
            PlaceEntity(i, M, new Rock());
        }

        for (int i = 0; i < M; i++)
        {
            PlaceEntity(-1, i, new Rock());
            PlaceEntity(N, i, new Rock());
        }
    }
}