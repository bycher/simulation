using Serilog;
using Simulation.Models.Actions;
using Simulation.Models.Options;
using Simulation.Services.Interfaces;
using Action = Simulation.Models.Actions.Action;

namespace Simulation.Models;

public sealed class Simulation : IDisposable
{
    private readonly Map _map;
    private int _iterationNum;

    private readonly List<Action> _initActions;
    private readonly List<Action> _turnActions;

    private readonly IMapRenderer _mapRenderer;
    private readonly ILogger _logger;

    private readonly ManualResetEvent _pauseEvent = new(true);
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public bool IsCancelled => _cancellationTokenSource.IsCancellationRequested;

    public Simulation(SimulationOptions options, IMapRenderer mapRenderer, ILogger logger)
    {
        _map = new(options.Rows, options.Columns);
        _mapRenderer = mapRenderer;
        _logger = logger;

        _initActions = ConfigureInitActions(options);
        _turnActions = ConfigureTurnActions(options);
    }

    public int Start()
    {
        try
        {
            Initialize();

            while (!IsCancelled)
            {
                StartNewIteration();
                Thread.Sleep(1000);
            }
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            _cancellationTokenSource.Cancel();
            return 1;
        }

        return 0;
    }

    public void TogglePause()
    {
        if (_pauseEvent.WaitOne(0))
        {
            _logger.Information("Simulation is paused...");
            _pauseEvent.Reset();
        }
        else
        {
            _logger.Information("Simulation is resumed...");
            _pauseEvent.Set();
        }
    }

    public void Stop()
    {
        _logger.Information("Simulation is stopped!");
        _pauseEvent.Set(); // resume if paused
        _cancellationTokenSource.Cancel();
    }

    private void Initialize()
    {
        _logger.Information("Perform initialization...");

        foreach (var action in _initActions)
            action.Execute(_map, CancellationToken.None);

        _logger.Information("Initialization is complete!");

        _mapRenderer.Render(_map);
    }

    private List<Action> ConfigureInitActions(SimulationOptions options) =>
    [
        new ArrangeRocks(options.RockOptions),
        new ArrangeTrees(options.TreeOptions),
        new ArrangeGrass(options.GrassOptions),
        new ArrangeHerbivores(options.HerbivoreOptions, _map, _logger),
        new ArrangePredators(options.PredatorOptions, _map, _logger),
    ];

    private List<Action> ConfigureTurnActions(SimulationOptions options) =>
    [
        new MoveCreatures(_mapRenderer, _pauseEvent),
        new GenerateLackingGrass(options.GrassOptions),
        new GenerateLackingHerbivores(options.HerbivoreOptions, _map, _logger),
    ];

    private void StartNewIteration()
    {
        _iterationNum++;

        _logger.Information($"Starting iteration #{_iterationNum}...");
        foreach (var action in _turnActions)
        {
            _pauseEvent.WaitOne();
            if (IsCancelled)
                return;

            action.Execute(_map, _cancellationTokenSource.Token);
        }
        _logger.Information($"Iteration #{_iterationNum} is complete!");

        _mapRenderer.Render(_map);
    }

    public void Dispose()
    {
        _pauseEvent.Dispose();
        _cancellationTokenSource.Dispose();
    }
}