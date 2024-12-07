using Simulation.Utility;
using Spectre.Console.Cli;

namespace Simulation;

internal class Program
{
    public static async Task<int> Main(string[] args)
    {
        var app = new CommandApp();
        app.Configure(config => config.AddCommand<SimulateCommand>("simulate"));

        return await app.RunAsync(args);
    }
}