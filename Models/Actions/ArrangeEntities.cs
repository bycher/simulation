namespace Simulation.Models.Actions;

public class ArrangeEntities<T> : Action where T : Entity
{
    private readonly int _entitiesNumber;
    private readonly Func<Position, T> _entityFactory;
    private readonly Random _random = new();

    public ArrangeEntities(
        Map map, int entitiesNumber, Func<Position, T> entityFactory) : base(map)
    {
        _entitiesNumber = entitiesNumber;
        _entityFactory = entityFactory;
    }

    public override void Execute()
    {
        for (int i = 0; i < _entitiesNumber; i++)
        {
            var position = GetRandomPosition();
            _map.PlaceEntity(position, _entityFactory(position));
        }
    }

    private Position GetRandomPosition()
    {
        Position position;
        do
        {
            int x = _random.Next(_map.N);
            int y = _random.Next(_map.M);
            position = new Position(x, y);
        }
        while (!_map.IsPositionFree(position));
        
        return position;
    }
}