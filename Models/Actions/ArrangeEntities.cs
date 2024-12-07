using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public abstract class ArrangeEntities<T> : Action where T : Entity
{
    protected readonly EntityOptions _options;
    protected Position _position = null!;

    public ArrangeEntities(EntityOptions options)
    {
        _options = options;
    }

    public abstract T CreateEntity();

    public override void Execute(Map map, CancellationToken cancellationToken)
    {
        for (int i = 0; i < _options.Number; i++)
        {
            if (cancellationToken.IsCancellationRequested)
                return;
            if (map.IsFilledIn)
                throw new Exception($"Arrangement of {typeof(T).Name} is failed: map is filled in");
                
            _position = GeneratePosition(map);
            map.PlaceEntity(_position, CreateEntity());
        }
    }

    private static Position GeneratePosition(Map map)
    {
        Position position;
        do
        {
            var random = new Random();
            position = new Position(random.Next(map.Rows), random.Next(map.Columns));
        }
        while (!map.IsPositionFree(position));

        return position;
    }
}