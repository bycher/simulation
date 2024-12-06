using Serilog;
using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public abstract class GenerateLackingResources<T>(EntityOptions options) : Action where T : Entity
{
    private readonly EntityOptions _options = options;

    protected abstract ArrangeEntities SetArrangeAction(Map map, EntityOptions newOptions);

    public override void Execute(Map map, ref bool isCancelled)
    {
        var resourceCount = map.GetEntities<T>().Count;
        var newOptions = _options with { Number = _options.Number - resourceCount };
        var arrangeAction = SetArrangeAction(map, newOptions);
        arrangeAction.Execute(map, ref isCancelled);
    }
}