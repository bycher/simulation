using System.Text;
using Simulation.Models;

namespace Simulation.Services;

public class MapRenderer
{
    private readonly Map _map;

    public MapRenderer(Map map)
    {
        _map = map;
    }

    public void Render()
    {
        Console.Clear();

        for (var x = 0; x < _map.N; x++)
        {
            for (var y = 0; y < _map.M; y++)
            {
                var cell = _map.GetCell(x, y);

                if (y > 0)
                    Console.Write(" ");
                Console.Write($"{GetCellState(cell), 4}");
            }

            Console.WriteLine();
        }
    }

    private static string GetCellState(Cell? cell)
    {
        if (cell is null)
            return ".";
        
        var state = new StringBuilder();
        foreach (var property in cell.GetType().GetProperties())
        {
            var value = property.GetValue(cell);
            if (value != null)
                state = state.Append(GetEntityImage((Entity)value));
        }

        return state.ToString();
    }

    private static string GetEntityImage(Entity entity) => entity switch
    {
        Rock => "#",
        Tree => "*",
        Grass => "^",
        Herbivore => "H",
        Predator => "P",
        _ => throw new ArgumentException("Unknown entity")
    };
}