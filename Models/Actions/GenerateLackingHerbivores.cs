using Serilog;
using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public class GenerateLackingHerbivores(CreatureOptions options, Map map, ILogger logger)
    : GenerateLackingResources<Herbivore>(options)
{
    private readonly Map _map = map;
    private readonly ILogger _logger = logger;

    protected override ArrangeEntities CreateArrangeAction(EntityOptions newOptions)
    {
        return new ArrangeHerbivores((CreatureOptions)newOptions, _map, _logger);
    }
}