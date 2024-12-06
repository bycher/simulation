using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public class GenerateLackingGrass(EntityOptions options) : GenerateLackingResources<Grass>(options)
{
    protected override ArrangeEntities CreateArrangeAction(EntityOptions newOptions)
    {
        return new ArrangeGrass(newOptions);
    }
}