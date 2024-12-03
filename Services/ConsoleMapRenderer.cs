using Simulation.Models;
using Simulation.Models.Entities;
using Simulation.Services.Interfaces;

namespace Simulation.Services;

public class ConsoleMapRenderer : IMapRenderer
{
    public void Render(Map map)
    {
        Console.Clear();

        for (int x = 0; x < map.N; x++)
        {
            for (int y = 0; y < map.M; y++)
            {
                if (y > 0)
                    Console.Write(" ");

                map.TryGetEntity(x, y, out var entity);
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