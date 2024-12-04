using System.Text.Json;
using Serilog;
using Simulation.Models;
using Simulation.Services;

internal class Program
{
    private static readonly string ConfigFilePath = Path.Combine("config", "options.json");
    
    private static readonly string LogsFilePath = Path.Combine(
        "logs", $"log_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

    private static readonly string LogTemplate = 
        "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

    public static void Main()
    {
        var options = GetSimulationOptions();
        if (options == null)
            return;

        var simulation = new Simulation.Models.Simulation(
            options,
            new ConsoleMapRenderer(),
            new LoggerConfiguration()
                .WriteTo.File(LogsFilePath, outputTemplate: LogTemplate)
                .CreateLogger());
                
        var simulationThread = new Thread(simulation.Start)
        {
            IsBackground = true
        };
        simulationThread.Start();

        HandleUserInput(simulation);
    }

    private static SimulationOptions? GetSimulationOptions()
    {
        SimulationOptions? options;
        using (var reader = new StreamReader(ConfigFilePath))
        {
            var json = reader.ReadToEnd();
            options = JsonSerializer.Deserialize<SimulationOptions>(json);
        }
        return options;
    }

    private static void HandleUserInput(Simulation.Models.Simulation simulation)
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