namespace Simulation.Models;

public class Map
{
    private readonly Dictionary<Position, Entity> _field;

    public int N { get; set; }
    public int M { get; set; }

    public Map(int n, int m)
    {
        N = n;
        M = m;
        _field = [];
    }

    public bool IsPositionTaken(Position position)
    {
        return _field.ContainsKey(position);
    }

    public void PlaceEntity(Position position, Entity entity)
    {
        _field[position] = entity;
    }

    public bool TryGetEntity(Position position, out Entity? entity)
    {
        entity = null;

        foreach (var key in _field.Keys)
        {
            if (key.Equals(position))
            {
                entity = _field[key];
                return true;
            }
        }

        return false;
    } 
}