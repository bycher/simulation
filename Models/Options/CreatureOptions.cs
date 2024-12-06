namespace Simulation.Models.Options;

public record CreatureOptions : EntityOptions
{
    public int Speed { get; init; }
    public int Health { get; init; }
}