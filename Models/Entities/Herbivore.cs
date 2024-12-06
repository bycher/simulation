using Serilog;
using Simulation.Models.Options;
using Simulation.Services.Interfaces;

namespace Simulation.Models.Entities;

public class Herbivore : Creature<Grass>
{
    public Herbivore(
        CreatureOptions options,
        Position currentPosition,
        IResourceSearcher resourceSearcher,
        ILogger logger
    ) : base(options, currentPosition, resourceSearcher, logger)
    {
    }

    protected override bool TryConsumeResource(Map map, Position position)
    {
        _logger.Information($"Herbivore found the grass at {position} and eat it");
        return map.RemoveEntity(position);
    }
}