using Simulation.Models;

internal class Program
{
    private static void Main(string[] args)
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
            HerbivoreHealth = 1,
        };

        var simulation = new Simulation.Models.Simulation(simulationParams);

        simulation.StartSimulation();
        simulation.StopSimulation();
    }
}