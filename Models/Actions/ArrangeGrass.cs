using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public class ArrangeGrass : ArrangeEntities<Grass>
{
    public ArrangeGrass(EntityOptions options) : base(options)
    {
    }

    public override Grass CreateEntity()
    {
        return new Grass(_options);
    }
}