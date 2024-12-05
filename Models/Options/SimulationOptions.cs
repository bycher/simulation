namespace Simulation.Models.Options;

public class SimulationOptions
{
    public int Rows { get; init; }
    public int Columns { get; init; }

    public required EntityOptions RockOptions { get; init; }
    public required EntityOptions TreeOptions { get; init; }
    public required EntityOptions GrassOptions { get; init; }

    public required CreatureOptions HerbivoreOptions { get; init; }
    public required PredatorOptions PredatorOptions { get; init; }
}