using Serilog;
using Simulation.Models.Entities;
using Simulation.Models.Options;
using Simulation.Services;

namespace Simulation.Models.Actions;

public class ArrangeHerbivores : ArrangeEntities
{
    private readonly Map _map;
    private readonly ILogger _logger;

    public ArrangeHerbivores(CreatureOptions options, Map map, ILogger logger) : base(options)
    {
        _map = map;
        _logger = logger;
    }

    public override Entity CreateEntity()
    {
        var resourceSearcher = new ResourceSearcher<Grass>(_map);
        return new Herbivore((CreatureOptions)_options, _position, resourceSearcher, _logger);
    }
}