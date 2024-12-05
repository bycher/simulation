using System.Text.Json;
using Serilog;
using Simulation.Models;
using Simulation.Services;

#pragma warning disable CS8604

internal class Program
{
    private static readonly string LogsFilePath = Path.Combine(
        "logs", $"log_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

    private static readonly string LogsTemplate = 
        "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

    public static void Main()
    {
        var configParser = new ConfigParser("default.json");
        if (configParser.TryParse(out var simulationOptions))
        {
            var simulation = new Simulation.Models.Simulation(
                simulationOptions,
                new ConsoleMapRenderer(),
                new LoggerConfiguration()
                    .WriteTo.File(LogsFilePath, outputTemplate: LogsTemplate)
                    .CreateLogger());

            simulation.Start();
            HandleUserInput(simulation);
        }
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