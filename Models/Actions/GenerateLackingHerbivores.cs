using Serilog;
using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public class GenerateLackingHerbivores : GenerateLackingResources<Herbivore>
{
    private readonly Map _map;
    private readonly ILogger _logger;

    public GenerateLackingHerbivores(CreatureOptions options, Map map, ILogger logger) : base(options)
    {
        _map = map;
        _logger = logger;
    }

    protected override ArrangeEntities CreateArrangeAction(EntityOptions newOptions)
    {
        return new ArrangeHerbivores((CreatureOptions)newOptions, _map, _logger);
    }
}