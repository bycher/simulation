using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public class ArrangeGrass : ArrangeEntities
{
    public ArrangeGrass(EntityOptions options) : base(options)
    {
    }

    public override Entity CreateEntity()
    {
        return new Grass(_options);
    }
}