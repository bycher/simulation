using System.ComponentModel;
using Spectre.Console.Cli;

namespace Simulation.Utility;

public class SimulateCommandSettings : CommandSettings
{
    [CommandOption("-c|--config <CONFIG_FILE>")]
    [Description("Configuration filename (.json) inside the config folder")]
    [DefaultValue("default.json")]

    public required string ConfigFile { get; set; }
}