using Simulation.Models;
using Simulation.Models.Entities;
using Simulation.Services.Interfaces;

namespace Simulation.Services;

public class ConsoleMapRenderer : IMapRenderer
{
    private const int CellWidth = 2;
    
    public void Render(Map map)
    {
        Console.Clear();

        PrintBorder(map.M);
        for (int x = 0; x < map.N; x++)
        {
            for (int y = 0; y < map.M; y++)
            {
                map.TryGetEntity(x, y, out var entity);
                var image = GetEntityImage(entity);
                Console.Write("| " + image.PadLeft((CellWidth + image.Length) / 2).PadRight(CellWidth));;
            }
            Console.WriteLine("|");
            PrintBorder(map.M);
        }
    }

    private static void PrintBorder(int columns)
    {
        for (int i = 0; i < columns; i++)
        {
            Console.Write("+".PadRight(CellWidth + 2, '-'));
        }
        Console.WriteLine("+");
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