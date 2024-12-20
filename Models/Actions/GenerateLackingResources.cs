using Serilog;
using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public abstract class GenerateLackingResources<T> : Action where T : Entity
{
    private readonly EntityOptions _options;

    public GenerateLackingResources(EntityOptions options)
    {
        _options = options;
    }

    protected abstract ArrangeEntities<T> CreateArrangeAction(EntityOptions newOptions);

    public override void Execute(Map map, CancellationToken cancellationToken)
    {
        var resourceCount = map.GetEntities<T>().Count;
        var arrangeAction = CreateArrangeAction(_options with
        {
            Number = _options.Number - resourceCount
        });

        arrangeAction.Execute(map, cancellationToken);
    }
}