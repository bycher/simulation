using Serilog;
using Serilog.Events;
using Simulation.Services;
using Simulation.Utility;

#pragma warning disable CS8604

internal class Program
{
    private static readonly string LogsFilePath = Path.Combine(
        "logs", $"log_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

    private static readonly string LogsFileTemplate = 
        "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

    public static async Task Main()
    {
        var logger = new LoggerConfiguration()
            .MinimumLevel.Is(
                System.Diagnostics.Debugger.IsAttached
                    ? LogEventLevel.Debug
                    : LogEventLevel.Information)
            .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Error)
            .WriteTo.File(LogsFilePath, outputTemplate: LogsFileTemplate)
            .CreateLogger();

        var configParser = new ConfigParser("default.json", logger);
        if (configParser.TryParse(out var simulationOptions))
        {
            var simulation = new Simulation.Models.Simulation(
                simulationOptions, new ConsoleMapRenderer(), logger);

            var simulationTask = Task.Run(simulation.Start);

            var inputListener = new InputListener(simulation);
            inputListener.Listen();

            await simulationTask;
        }
    }
}

#pragma warning restore CS8604