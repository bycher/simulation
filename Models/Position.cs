namespace Simulation.Models;

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public List<Position> Adjacents => [
        new Position(X + 1, Y),
        new Position(X, Y + 1),
        new Position(X - 1, Y),
        new Position(X, Y - 1),
    ];

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

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