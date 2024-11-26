namespace Simulation.Models;

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
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
}