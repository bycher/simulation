 namespace Simulation.Utility;

public class InputListener
{
    private readonly Models.Simulation _simulation;

    public InputListener(Models.Simulation simulation)
    {
        _simulation = simulation;
    }

    public void Listen()
    {
        while (!_simulation.IsCancelled)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Spacebar)
                    _simulation.TogglePause();
                if (key == ConsoleKey.Escape)
                    break;
            }
        }
        _simulation.Stop();
    }
}