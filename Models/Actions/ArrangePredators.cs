using Serilog;
using Simulation.Models.Entities;
using Simulation.Models.Options;
using Simulation.Services;

namespace Simulation.Models.Actions;

public class ArrangePredators(PredatorOptions options, Map map, ILogger logger) : ArrangeEntities(options)
{
    private readonly Map _map = map;
    private readonly ILogger _logger = logger;

    public override Entity CreateEntity()
    {
        var resourceSearcher = new ResourceSearcher<Herbivore>(_map);
        return new Predator((PredatorOptions)_options, _position, resourceSearcher, _logger);
    }
}