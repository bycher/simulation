namespace Simulation.Models.Actions;

public class ArrangeEntities<T>(int entitiesNumber, Func<Position, T> entityFactory) : Action
    where T : Entity
{
    private readonly int _entitiesNumber = entitiesNumber;
    private readonly Func<Position, T> _entityFactory = entityFactory;

    public override void Execute(Map map)
    {
        for (int i = 0; i < _entitiesNumber; i++)
        {
            var position = GetRandomPosition(map);
            map.PlaceEntity(position, _entityFactory(position));
        }
    }

    private static Position GetRandomPosition(Map map)
    {
        Position position;
        do
        {
            var random = new Random();
            position = new Position(random.Next(map.N),
                                    random.Next(map.M));
        }
        while (!map.IsPositionFree(position));
        
        return position;
    }
}