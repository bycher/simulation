using System.ComponentModel;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Simulation.Services;
using Spectre.Console.Cli;

namespace Simulation.Utility;

public class SimulateCommand : AsyncCommand<SimulateCommandSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, SimulateCommandSettings settings)
    {
        var logger = ConfigureLogger();

        var configParser = new ConfigParser(settings.ConfigFile, logger);
        if (configParser.TryParse(out var simulationOptions))
        {
            var simulation = new Models.Simulation(
                simulationOptions!, new ConsoleMapRenderer(), logger);
                
            var simulationTask = Task.Run(simulation.Start);

            var inputListener = new InputListener(simulation);  
            inputListener.Listen();
            
            return await simulationTask;
        }

        return 1;
    }

    private static Logger ConfigureLogger()
    {
        var logsFilePath = Path.Combine("logs", $"log_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
        var logsFileTemplate =
            "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

        return new LoggerConfiguration()
            .MinimumLevel.Is(
                System.Diagnostics.Debugger.IsAttached
                    ? LogEventLevel.Debug
                    : LogEventLevel.Information)
            .WriteTo.Console(LogEventLevel.Error)
            .WriteTo.File(logsFilePath, outputTemplate: logsFileTemplate)
            .CreateLogger();
    }
}