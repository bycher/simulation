using Serilog;
using Simulation.Models.Options;
using Simulation.Services.Interfaces;

namespace Simulation.Models.Entities;

public class Predator : Creature<Herbivore>
{
    public int Attack { get; set; }

    public Predator(
        PredatorOptions options,
        Position currentPosition,
        IResourceSearcher resourceSearcher,
        ILogger logger
    ) : base(options, currentPosition, resourceSearcher, logger)
    {
        Attack = options.Attack;
    }

    protected override bool TryConsumeResource(Map map, Position position)
    {
        if (!map.TryGetEntity(position, out var entity))
            throw new KeyNotFoundException($"No resource found at position {position}");

        var herbivore = (Herbivore)entity!;

        herbivore.Health -= Attack;
        if (herbivore.Health <= 0)
        {
            _logger.Information($"Predator killed the herbivore at {position}");
            return map.RemoveEntity(position);
        }

        _logger.Information(
            $"Predator at {_currentPosition} attacked the herbivore at {position} " +
            $"(remaining health: {herbivore.Health})"
        );
        return false;
    }
}