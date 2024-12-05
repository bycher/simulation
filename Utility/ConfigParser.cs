using System.Text.Json;
using Serilog;
using Simulation.Models;

namespace Simulation.Utility;

public class ConfigParser(string fileName, ILogger logger)
{
    private readonly string _path = Path.Combine("config", fileName);
    private readonly ILogger _logger = logger;

    public bool TryParse(out SimulationOptions? simulationOptions)
    {
        simulationOptions = null;
        if (!File.Exists(_path))
        {
            _logger.Error("Config file doesn't exist!");
            return false;
        }
        
        try
        {
            var json = File.ReadAllText(_path);
            ValidateJson(json);
            simulationOptions = JsonSerializer.Deserialize<SimulationOptions>(json);
        }
        catch (Exception e)
        {
            _logger.Error($"Config parsing is failed! {e.Message}");
            return false;
        }

        _logger.Information("Config file parsed successfully!");
        return true;
    }

    private static bool ValidateJson(string json)
    {
        var validPropertyNames = typeof(SimulationOptions).GetProperties().Select(p => p.Name);

        var document = JsonDocument.Parse(json);
        foreach (var property in document.RootElement.EnumerateObject())
        {
            if (!validPropertyNames.Contains(property.Name))
                throw new JsonException($"Unknown property: \"{property.Name}\"");
        } 
        return true;
    }
}