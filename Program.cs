using Simulation.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var options = new SimulationOptions
        {
            N = 10,
            M = 10,
            RocksNumber = 3,
        };

        var simulation = new Simulation.Models.Simulation(options);

        simulation.StartSimulation();
        simulation.StopSimulation();
    }
}