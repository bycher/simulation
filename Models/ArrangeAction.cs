namespace Simulation.Models;

public class ArrangeAction<T> : Action where T : Entity, new()
{
    private readonly int _limit;

    public ArrangeAction(Map map, int limit) : base(map)
    {
        _limit = limit;
    }

    public override void Act()
    {
        for (int i = 0; i < _limit; i++)
        {
            Position position;
            do
            {
                position = GeneratePosition();
            }
            while (_map.IsPositionTaken(position));
            
            _map.PlaceEntity(position, new T());
        }
    }

    private Position GeneratePosition()
    {
        var rand = new Random();

        var x = rand.Next(_map.N);
        var y = rand.Next(_map.M);

        return new(x, y);
    }
}