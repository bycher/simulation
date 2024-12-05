﻿using Serilog;
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

    public static void Main()
    {
        var logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Error)
            .WriteTo.File(LogsFilePath, outputTemplate: LogsFileTemplate)
            .CreateLogger();

        var configParser = new ConfigParser("default.json", logger);
        if (configParser.TryParse(out var simulationOptions))
        {
            var simulation = new Simulation.Models.Simulation(
                simulationOptions, new ConsoleMapRenderer(), logger);
            
            var inputListener = new InputListener(simulation);
            inputListener.Listen(); // starts as background thread

            simulation.Start();
        }
    }
}

#pragma warning restore CS8604