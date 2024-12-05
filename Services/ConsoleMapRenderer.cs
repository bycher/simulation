using Simulation.Models;
using Simulation.Services.Interfaces;

namespace Simulation.Services;

public class ConsoleMapRenderer : IMapRenderer
{
    private const int CellWidth = 2;

    public void Render(Map map)
    {
        Console.Clear();

        PrintBorder(map.Columns);
        for (int x = 0; x < map.Rows; x++)
        {
            for (int y = 0; y < map.Columns; y++)
            {
                var image = map.TryGetEntity(x, y, out var entity)
                    ? entity!.Image
                    : " ";
                Console.Write($"| {image.PadLeft((CellWidth + image.Length) / 2),-CellWidth} ");
            }
            Console.WriteLine("|");
            PrintBorder(map.Columns);
        }
    }

    private static void PrintBorder(int columns)
    {
        for (int i = 0; i < columns; i++)
            Console.Write("+".PadRight(CellWidth + 3, '-'));
        Console.WriteLine("+");
    }
}