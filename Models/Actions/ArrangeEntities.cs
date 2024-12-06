using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public abstract class ArrangeEntities(EntityOptions options) : Action
{
    protected readonly EntityOptions _options = options;
    protected Position _position = null!;

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