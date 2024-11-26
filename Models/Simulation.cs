using Simulation.Services;

namespace Simulation.Models;

public class Simulation
{
    public Map Map { get; set; }
    public MapRenderer MapRenderer { get; set; }

    public List<Action> InitActions { get; set; }
    public List<Action> TurnActions { get; set; }
    
    public int TurnsCount { get; set; }

    private bool _isRunning = true;

    public Simulation(SimulationOptions options)
    {
        Map = new(options.N, options.M);
        MapRenderer = new(Map);

        InitActions = [
            new ArrangeAction<Rock>(Map, options.RocksNumber),
        ];

        TurnActions = [];
    }

    public void StartSimulation()
    {
        // do init actions
        foreach (var action in InitActions)
            action.Act();

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
        // do turn actions
        foreach (var action in TurnActions)
            action.Act();
        
        // do rendering 
        MapRenderer.Render();
    }

    public void StopSimulation()
    {
        _isRunning = false;
    }
}