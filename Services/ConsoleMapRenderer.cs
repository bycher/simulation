using Simulation.Models;
using Simulation.Services.Interfaces;
using Spectre.Console;

namespace Simulation.Services;

public class ConsoleMapRenderer : IMapRenderer
{
    public void Render(Map map)
    {
        Console.Clear();

        var table = new Table
        {
            Border = TableBorder.DoubleEdge,
            ShowRowSeparators = true,
        };

        table.AddColumn(new TableColumn(" "));
        for (int i = 1; i <= map.Columns; i++)
            table.AddColumn($"[yellow]{i}[/]");
        
        for (int x = 0; x < map.Rows; x++)
        {
            var columns = new List<string> { $"[yellow]{x + 1}[/]" };

            for (int y = 0; y < map.Columns; y++)
            {
                var image = map.TryGetEntity(x, y, out var entity)
                    ? entity!.Image
                    : " ";
                columns.Add(image);
            }
            table.AddRow(columns.ToArray());
        }

        AnsiConsole.Write(table);
    }
}