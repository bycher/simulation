using Simulation.Models;

namespace Simulation.Services;

public interface IResourceSearcher
{
    void Reset();
    List<Position> FindResource(Position start);
}