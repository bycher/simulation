using Simulation.Models.Actions;
using Simulation.Models.Entities;
using Simulation.Services;

namespace Simulation.Models;

public class Simulation
{
    public Map Map { get; set; }
    public ConsoleRenderer MapRenderer { get; set; }
    public List<Actions.Action> InitActions { get; set; }
    public List<Actions.Action> TurnActions { get; set; }
    public int TurnsCount { get; set; }

    private readonly ManualResetEvent _pauseEvent = new(true);

    public Simulation(SimulationParams options)
    {
        Map = new(options.N, options.M);
        MapRenderer = new(Map);

        InitActions = [
            new ArrangeEntities<Rock>(options.RocksNumber, _ => new Rock()),
            new ArrangeEntities<Tree>(options.TreeNumber, _ => new Tree()),
            new ArrangeEntities<Grass>(options.GrassNumber, _ => new Grass()),
            new ArrangeEntities<Herbivore>(
                options.HerbivoreNumber,
                position => new Herbivore(
                    options.HerbivoreSpeed,
                    options.HerbivoreHealth,
                    position,
                    new ResourceSearcher<Grass>(Map))),
            new ArrangeEntities<Predator>(
                options.PredatorNumber,
                position => new Predator(
                    options.PredatorSpeed,
                    options.PredatorHealth,
                    options.PredatorAttack,
                    position,
                    new ResourceSearcher<Herbivore>(Map))),
        ];

        TurnActions = [
            new MoveCreatures(MapRenderer, _pauseEvent),
        ];
    }

    public void Start()
    {
        new Thread(() =>
        {
            foreach (var action in InitActions)
                action.Execute(Map);

            MapRenderer.Render();

            while (true)
            {
                NextTurn();
                MapRenderer.Render();
                Thread.Sleep(1000);
            }
        })
        {
            IsBackground = true
        }
        .Start();
        ListenForInput();
    }

    private void ListenForInput()
    {
        // Слушаем ввод пользователя в отдельном потоке
        new Thread(() =>
        {
            while (true)
            {
                var key = Console.ReadKey(intercept: true).Key;
                if (key == ConsoleKey.Spacebar) // Например, пробел для паузы/возобновления
                {
                    if (_pauseEvent.WaitOne(0)) // Если не на паузе
                    {
                        Console.WriteLine("Симуляция на паузе...");
                        _pauseEvent.Reset(); // Ставим на паузу
                    }
                    else
                    {
                        Console.WriteLine("Симуляция возобновлена...");
                        _pauseEvent.Set(); // Снимаем с паузы
                    }
                }
            }
        }).Start();
    }

    private void NextTurn()
    {
        TurnsCount++;

        foreach (var action in TurnActions)
        {
            _pauseEvent.WaitOne();
            action.Execute(Map);
        }
    }
}