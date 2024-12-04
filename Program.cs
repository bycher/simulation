using System.Text.Json;
using Serilog;
using Serilog.Core;
using Simulation.Models;
using Simulation.Services;

internal class Program
{
    public static void Main()
    {
        SimulationParams? options;
        using (var reader = new StreamReader("config/options.json"))
        {
            var json = reader.ReadToEnd();
            options = JsonSerializer.Deserialize<SimulationParams>(json);
        }

        var logger = new LoggerConfiguration()
            .WriteTo.File(
                path: $"logs/log_{DateTime.Now:yyyyMMdd_HHmmss}.txt",
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        var mapRenderer = new ConsoleMapRenderer();

        var simulation = new Simulation.Models.Simulation(options!, mapRenderer, logger);
        var simulationThread = new Thread(simulation.Start)
        {
            IsBackground = true
        };
        simulationThread.Start();

        ListenForInput(simulation);
    }

    private static void ListenForInput(Simulation.Models.Simulation simulation)
    {
        ConsoleKey key;
        do
        {
            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Spacebar)
                simulation.TogglePause();
        }
        while (key != ConsoleKey.Escape);

        simulation.Stop();
    }
}