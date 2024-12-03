using Simulation.Models;
using Simulation.Services.Interfaces;

namespace Simulation.Services;

public class ConsoleRenderer(Map map) : IMapRenderer
{
    private readonly Map _map = map;

    public void Render()
    {
        Console.Clear();

        for (int x = 0; x < _map.N; x++)
        {
            for (int y = 0; y < _map.M; y++)
            {
                if (y > 0)
                    Console.Write(" ");

                _map.TryGetEntity(x, y, out var entity);
                Console.Write(GetEntityImage(entity));
            }
            Console.WriteLine();
        }
    }

    private static string GetEntityImage(Entity? entity) => entity switch
    {
        null => ".",
        Rock => "#",
        Tree => "*",
        Grass => "^",
        Herbivore => "H",
        Predator => "P",
        _ => throw new ArgumentException("Unknown entity")
    };
}