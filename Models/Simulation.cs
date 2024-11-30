using Simulation.Models.Actions;
using Simulation.Services;

namespace Simulation.Models;

public class Simulation
{
    private bool _isRunning = true;

    public Map Map { get; set; }
    public MapRenderer MapRenderer { get; set; }
    public List<Actions.Action> InitActions { get; set; }
    public List<Actions.Action> TurnActions { get; set; }
    public int TurnsCount { get; set; }

    public Simulation(SimulationParams options)
    {
        Map = new(options.N, options.M);
        MapRenderer = new(Map);

        InitActions = [
            new ArrangeEntities<Rock>(Map, options.RocksNumber, _ => new Rock()),
            new ArrangeEntities<Tree>(Map, options.TreeNumber, _ => new Tree()),
            new ArrangeEntities<Grass>(Map, options.GrassNumber, _ => new Grass()),
            new ArrangeEntities<Herbivore>(
                Map,
                options.HerbivoreNumber,
                position => new Herbivore(
                    options.HerbivoreSpeed,
                    options.HerbivoreHealth,
                    Map,
                    position)),
        ];

        TurnActions = [
            new MoveCreatures(Map, MapRenderer),
        ];
    }

    public void StartSimulation()
    {
        // do init actions
        foreach (var action in InitActions)
            action.Execute();

        // do initial rendering 
        MapRenderer.Render();

        // start simulation
        while (_isRunning)
        {
            Thread.Sleep(1000);
            NextTurn();
        }
    }

    public void NextTurn()
    {
        TurnsCount++;

        // do turn actions
        foreach (var action in TurnActions)
            action.Execute();

        // do rendering 
        MapRenderer.Render();
    }

    public void StopSimulation()
    {
        _isRunning = false;
    }
}