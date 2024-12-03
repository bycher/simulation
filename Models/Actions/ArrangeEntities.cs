namespace Simulation.Models.Actions;

public class ArrangeEntities<T>(
    Map map, int entitiesNumber, Func<Position, T> entityFactory) : Action(map)
    where T : Entity
{
    private readonly int _entitiesNumber = entitiesNumber;
    private readonly Func<Position, T> _entityFactory = entityFactory;
    private readonly Random _random = new();

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