using Serilog;
using Simulation.Models.Entities;
using Simulation.Models.Options;
using Simulation.Services;

namespace Simulation.Models.Actions;

public class ArrangeHerbivores(CreatureOptions options, Map map, ILogger logger) : ArrangeEntities(options)
{
    private readonly Map _map = map;
    private readonly ILogger _logger = logger;

    public override Entity CreateEntity()
    {
        var resourceSearcher = new ResourceSearcher<Grass>(_map);
        return new Herbivore((CreatureOptions)_options, _position, resourceSearcher, _logger);
    }
}