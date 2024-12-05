using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public class ArrangeGrass(EntityOptions options) : ArrangeEntities(options)
{
    public override Entity CreateEntity()
    {
        return new Grass(_options);
    }
}