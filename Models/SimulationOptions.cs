using Simulation.Models.Options;

namespace Simulation.Models;

#pragma warning disable CS8618

public class SimulationOptions
{
    public int Rows { get; set; }
    public int Columns { get; set; }

    public EntityOptions RockOptions { get; set; }
    public EntityOptions TreeOptions { get; set; }
    public EntityOptions GrassOptions { get; set; }

    public CreatureOptions HerbivoreOptions { get; set; }
    public PredatorOptions PredatorOptions { get; set; }
}

#pragma warning restore CS8618