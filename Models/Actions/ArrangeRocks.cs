using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public class ArrangeRocks(EntityOptions options) : ArrangeEntities(options)
{
    public override Entity CreateEntity()
    {
        return new Rock(_options);
    }
}