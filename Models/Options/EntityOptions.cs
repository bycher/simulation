namespace Simulation.Models.Options;

public record EntityOptions
{
    public int Number { get; init; }
    public required string Image { get; init; }
}