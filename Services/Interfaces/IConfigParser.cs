using Simulation.Models;

namespace Simulation.Services.Interfaces;

public interface IConfigParser
{
    bool TryParse(out SimulationOptions? simulationOptions);
}