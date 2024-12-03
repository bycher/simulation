using Serilog;
using Simulation.Models;
using Simulation.Services;

internal class Program
{
    private static void Main()
    {
        var simulationParams = new SimulationParams
        {
            N = 10,
            M = 10,
            RocksNumber = 3,
            TreeNumber = 3,
            GrassNumber = 5,
            HerbivoreNumber = 3,
            HerbivoreSpeed = 1,
            HerbivoreHealth = 4,
            PredatorNumber = 3,
            PredatorSpeed = 2,
            PredatorHealth = 1,
            PredatorAttack = 2,
        };

        var logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        var simulation = new Simulation.Models.Simulation(
            simulationParams, new ConsoleMapRenderer(), logger);

        simulation.Start();
    }
}