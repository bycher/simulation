using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public abstract class ArrangeEntities : Action
{
    protected readonly EntityOptions _options;
    protected Position _position = null!;

    public ArrangeEntities(EntityOptions options)
    {
        _options = options;
    }

    public abstract Entity CreateEntity();

    public override void Execute(Map map, CancellationToken cancellationToken)
    {
        for (int i = 0; i < _options.Number && !cancellationToken.IsCancellationRequested; i++)
        {
            do
            {
                var random = new Random();
                _position = new Position(random.Next(map.Rows), random.Next(map.Columns));
            }
            while (!map.IsPositionFree(_position));

            map.PlaceEntity(_position, CreateEntity());
        }
    }
}