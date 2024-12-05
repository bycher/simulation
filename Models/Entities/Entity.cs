using Simulation.Models.Options;

namespace Simulation.Models.Entities;

public abstract class Entity(EntityOptions options)
{
    public string Image { get; set; } = options.Image;
}