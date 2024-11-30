namespace Simulation.Models;

public class Map
{
    public int N { get; set; }
    public int M { get; set; }

    public List<Creature> Creatures => _entities.Values
                                                .SelectMany(c => new List<Creature?> { c.Herbivore, c.Predator })
                                                .Where(c => c != null)
                                                .Cast<Creature>()
                                                .ToList();

    private readonly Dictionary<Position, Cell> _entities = [];

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
        var cell = GetCell(position);
        
        if (cell is null)
            _entities[position] = new Cell();
        
        foreach (var property in _entities[position].GetType().GetProperties())
        {
            if (property.PropertyType == entity.GetType())
                property.SetValue(_entities[position], entity);
        }
    }

    public void PlaceEntity(int x, int y, Entity entity)
    {
        PlaceEntity(new Position(x, y), entity);
    }

    public Cell? GetCell(int x, int y)
    {
        return GetCell(new Position(x, y));
    }

    public Cell? GetCell(Position position)
    {
        _entities.TryGetValue(position, out var value);
        return value;
    }

    public void RemoveEntity(Position position, Type entityType)
    {
        var cell = GetCell(position) ??
            throw new NullReferenceException("No entities at position");

        var properties = cell.GetType().GetProperties();
        foreach (var property in properties)
        {
            if (property.PropertyType == entityType)
                property.SetValue(cell, null);
        }

        bool needEliminate = true;
        foreach (var property in properties)
            if (property.GetValue(cell) != null)
                needEliminate = false;
        if (needEliminate)
            _entities.Remove(position);
    }

    public bool CanBePassedThrough(Position position, Entity subject)
    {
        var cell = GetCell(position);

        return cell is null ||
               cell.Rock is null && cell.Tree is null &&
               (subject is Herbivore && cell.Herbivore == null && cell.Predator == null ||
                subject is Predator && cell.Predator == null);
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