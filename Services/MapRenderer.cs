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
                var entities = _map.GetEntitiesAtPosition(x, y);

                if (y > 0)
                    Console.Write(" ");
                Console.Write(GetCharForCell(entities));
            }

            Console.WriteLine();
        }
    }

    private static char GetCharForCell(List<Entity>? entities)
    {
        if (entities is null)
            return '.';
        
        if (entities.Count == 2)
            return '\u1e2a';
        
        return entities[0] switch 
        {
            Rock => '#',
            Tree => '*',
            Grass => '^',
            Herbivore => 'H',
            _ => throw new ArgumentException("Unknown entity")
        };
    }
}