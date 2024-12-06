namespace Simulation.Models;

public class Position(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    public List<Position> Adjacents => [
        new Position(X + 1, Y),
        new Position(X, Y + 1),
        new Position(X - 1, Y),
        new Position(X, Y - 1),
    ];

    public bool IsInsideMap(Map map)
    {
        return X >= 0 && X < map.Rows && Y >= 0 && Y < map.Columns;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Position position)
            return position.X == X && position.Y == Y;
            
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"({X}; {Y})";
    }
}