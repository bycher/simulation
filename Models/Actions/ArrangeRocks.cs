using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public class ArrangeRocks : ArrangeEntities<Rock>
{
    public ArrangeRocks(EntityOptions options) : base(options)
    {
    }

    public override Rock CreateEntity()
    {
        return new Rock(_options);
    }
}