using Serilog;
using Simulation.Models.Actions;
using Simulation.Models.Entities;
using Simulation.Services;
using Simulation.Services.Interfaces;
using Action = Simulation.Models.Actions.Action;

namespace Simulation.Models;

public class Simulation
{
    private readonly Map _map;
    private readonly IMapRenderer _mapRenderer;
    private readonly ILogger _logger;
    private int _iterationNum;
    private bool _isCancelled = false;

    private readonly List<Action> _initActions;
    private readonly List<Action> _turnActions;

    private readonly ManualResetEvent _pauseEvent = new(true);

    public Simulation(SimulationOptions options, IMapRenderer mapRenderer, ILogger logger)
    {
        _map = new(options.N, options.M);
        _mapRenderer = mapRenderer;
        _logger = logger;

        _initActions = [
            new ArrangeEntities<Rock>(options.RocksNumber, _ => new Rock()),
            new ArrangeEntities<Tree>(options.TreeNumber, _ => new Tree()),
            new ArrangeEntities<Grass>(options.GrassNumber, _ => new Grass()),
            new ArrangeEntities<Herbivore>(
                options.HerbivoreNumber,
                position => new Herbivore(
                    options.HerbivoreSpeed,
                    options.HerbivoreHealth,
                    position,
                    new ResourceSearcher<Grass>(_map),
                    logger)),
            new ArrangeEntities<Predator>(
                options.PredatorNumber,
                position => new Predator(
                    options.PredatorSpeed,
                    options.PredatorHealth,
                    options.PredatorAttack,
                    position,
                    new ResourceSearcher<Herbivore>(_map),
                    logger)),
        ];

        _turnActions = [
            new MoveCreatures(_mapRenderer, _pauseEvent),
        ];
    }

    public void Start()
    {
        _logger.Information("Simulation is started");

        Initialize();

        while (!_isCancelled)
        {
            StartNewIteration();
            Thread.Sleep(1000);
        }
    }

    public void TogglePause()
    {
        if (_pauseEvent.WaitOne(0))
        {
            _logger.Information("Simulation is paused");
            _pauseEvent.Reset();
        }
        else
        {
            _logger.Information("Simulation is resumed");
            _pauseEvent.Set();
        }
    }

    public void Stop()
    {
        _logger.Information("Simulation is stopped");
        _isCancelled = true;
    }

    public void Initialize()
    {
        _logger.Information("Perform initialization");
        foreach (var action in _initActions)
        {
            action.Execute(_map, ref _isCancelled);
        }
        _logger.Information("Initialization is complete");

        _mapRenderer.Render(_map);
    }

    private void StartNewIteration()
    {
        _iterationNum++;

        _logger.Information($"Starting iteration #{_iterationNum}");
        foreach (var action in _turnActions)
        {
            _pauseEvent.WaitOne();
            action.Execute(_map, ref _isCancelled);
            if (_isCancelled)
                return;
        }
        _logger.Information($"Iteration #{_iterationNum} is complete");

        _mapRenderer.Render(_map);
    }
}