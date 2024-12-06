using Simulation.Models.Options;

namespace Simulation.Models.Entities;

public abstract class Entity
{
    public string Image { get; set; }

    public Entity(EntityOptions options)
    {
        Image = options.Image;
    }
}