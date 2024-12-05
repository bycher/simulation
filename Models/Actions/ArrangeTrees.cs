using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public class ArrangeTrees(EntityOptions options) : ArrangeEntities(options)
{
    public override Entity CreateEntity()
    {
        return new Tree(_options);
    }
}