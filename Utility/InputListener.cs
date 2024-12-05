 namespace Simulation.Utility;

public class InputListener(Models.Simulation simulation)
{
    private readonly Models.Simulation _simulation = simulation;

    public void Listen()
    {
        new Thread(() =>
        {
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Spacebar)
                {
                    _simulation.TogglePause();
                }
            }
            while (key != ConsoleKey.Escape);

            _simulation.Stop();
        })
        {
            IsBackground = true
        }
        .Start();
    }
}