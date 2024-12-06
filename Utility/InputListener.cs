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
        ConsoleKey key;
        do
        {
            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Spacebar)
                _simulation.TogglePause();
        }
        while (key != ConsoleKey.Escape);

        _simulation.Stop();
    }
}