using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public class ArrangeRocks : ArrangeEntities
{
    public ArrangeRocks(EntityOptions options) : base(options)
    {
    }

    public override Entity CreateEntity()
    {
        return new Rock(_options);
    }
}