using System.Text.Json;
using Serilog;
using Simulation.Models;
using Simulation.Services.Interfaces;

namespace Simulation.Services;

public class ConfigParser(string fileName) : IConfigParser
{
    private readonly string _path = Path.Combine("config", fileName);

    public bool TryParse(out SimulationOptions? simulationOptions)
    {
        simulationOptions = null;
        if (!File.Exists(_path))
        {
            Console.WriteLine("Error: Config file doesn't exist!");
            return false;
        }
        
        using var reader = new StreamReader(_path);
        var json = reader.ReadToEnd();
        try
        {
            simulationOptions = JsonSerializer.Deserialize<SimulationOptions>(json);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: JSON deserialization is failed! " + e);
            return false;
        }
        return true;
    }
}