using Simulation.Models.Entities;
using Simulation.Models.Options;

namespace Simulation.Models.Actions;

public class GenerateLackingGrass : GenerateLackingResources<Grass>
{
    public GenerateLackingGrass(EntityOptions options) : base(options)
    {
    }

    protected override ArrangeEntities CreateArrangeAction(EntityOptions newOptions)
    {
        return new ArrangeGrass(newOptions);
    }
}