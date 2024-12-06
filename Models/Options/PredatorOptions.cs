namespace Simulation.Models.Options;

public record PredatorOptions : CreatureOptions
{
    public int Attack { get; init; }
}