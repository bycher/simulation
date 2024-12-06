using System.Text.Json;
using Serilog;
using Simulation.Models.Options;

namespace Simulation.Utility;

public class ConfigParser
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    private readonly string _path;
    private readonly ILogger _logger;

    public ConfigParser(string fileName, ILogger logger)
    {
        _path = Path.Combine("config", fileName);
        _logger = logger;
    }

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
            simulationOptions = JsonSerializer.Deserialize<SimulationOptions>(json, _serializerOptions);
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
        var validPropertyNames = typeof(SimulationOptions)
            .GetProperties()
            .Select(p => JsonNamingPolicy.CamelCase.ConvertName(p.Name));

        var document = JsonDocument.Parse(json);
        foreach (var property in document.RootElement.EnumerateObject())
        {
            if (!validPropertyNames.Contains(property.Name))
                throw new JsonException($"Unknown property: \"{property.Name}\"");
        }
        return true;
    }
}