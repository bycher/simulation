using System.Linq.Expressions;
using System.Text.Json;
using Serilog;
using Simulation.Models.Options;

namespace Simulation.Utility;

public class ConfigParser
{
    private const string ConfigDirectory = "config";

    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    private readonly string _configPath;
    private readonly ILogger _logger;

    public ConfigParser(string fileName, ILogger logger)
    {
        _configPath = Path.Combine(ConfigDirectory, fileName);
        _logger = logger;
    }

    public bool TryParse(out SimulationOptions? simulationOptions)
    {
        simulationOptions = null;
        if (!File.Exists(_configPath))
        {
            _logger.Error("Config file doesn't exist!");
            return false;
        }

        try
        {
            var json = File.ReadAllText(_configPath);
            ValidateJson(json);

            simulationOptions = JsonSerializer.Deserialize<SimulationOptions>(json, _serializerOptions)
                                ?? throw new JsonException("Simulation options is null");

            ValidateSimulationOptions(simulationOptions);
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

    private static bool ValidateSimulationOptions(SimulationOptions options)
    {
        // Validate map size
        EnsureGreaterThanZero(options.Rows, nameof(options.Rows));
        EnsureGreaterThanZero(options.Columns, nameof(options.Columns));

        var entitiesOptionsProperties = options.GetType()
            .GetProperties()
            .Where(p => p.PropertyType.Name.EndsWith("Options"))
            .ToList();

        // Validate options for all antities
        foreach (var property in entitiesOptionsProperties)
        {
            var entityOptions = (EntityOptions)property.GetValue(options)!;
            ValidateEntityOptions(entityOptions);
        }

        return true;
    }

    private static bool ValidateEntityOptions(EntityOptions options)
    {
        var optionsType = options.GetType();
        var propertiesToValidate = optionsType.GetProperties()
            .Where(p => p.PropertyType == typeof(int)); // don't need to validate "Image"

        foreach (var property in propertiesToValidate)
        {
            var val = (int)property.GetValue(options)!;
            EnsureGreaterThanZero(val, $"{optionsType.Name}.{property.Name}");
        }
        
        return true;
    }

    private static bool EnsureGreaterThanZero(int value, string paramName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(
                paramName, value, $"{paramName} must be greater than 0");

        return true;
    }

}