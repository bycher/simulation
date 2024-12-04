using Serilog;
using Simulation.Services;

namespace Simulation.Models.Entities;

public class Herbivore(int speed, int health, Position currentPosition,
                    IResourceSearcher resourceSearcher, ILogger logger)
    : Creature<Grass>(speed, health, currentPosition, resourceSearcher, logger)
{
    protected override bool TryConsumeResource(Map map, Position position)
    {
        _logger.Information($"Herbivore found the grass at {position} and eat it");
        map.RemoveEntity(position);
        FindNewPath(position);
        return true;
    }

    public override string ToString() => "\U0001F42E";
}