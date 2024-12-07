using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public class ArrangeTrees : ArrangeEntities<Tree>
{
    public ArrangeTrees(EntityOptions options) : base(options)
    {
    }

    public override Tree CreateEntity()
    {
        return new Tree(_options);
    }
}