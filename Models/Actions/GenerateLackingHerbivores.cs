using Serilog;
using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public class GenerateLackingHerbivores(CreatureOptions options, ILogger logger)
    : GenerateLackingResources<Herbivore>(options)
{
    private readonly ILogger _logger = logger;

    protected override ArrangeEntities SetArrangeAction(Map map, EntityOptions newOptions)
    {
        return new ArrangeHerbivores((CreatureOptions)newOptions, map, _logger);
    }
}