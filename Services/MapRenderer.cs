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
        
        for (int i = 0; i < _map.N; i++)
        {
            for (int j = 0; j < _map.M; j++)
            {
                if (!_map.TryGetEntity(new Position(i, j), out var entity))
                    Console.Write(".");
                else
                    Console.Write(GetImageForEntity(entity));
            }
            Console.WriteLine();
        }
    }

    private static string GetImageForEntity(Entity? entity)
    {
        if (entity is Rock)
            return "#";

        throw new ArgumentException("Unknown entity");
    }
}