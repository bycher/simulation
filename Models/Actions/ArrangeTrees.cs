using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public class ArrangeTrees : ArrangeEntities
{
    public ArrangeTrees(EntityOptions options) : base(options)
    {
    }

    public override Entity CreateEntity()
    {
        return new Tree(_options);
    }
}