using Simulation.Models;

namespace Simulation.Services.Interfaces;

public interface IResourceSearcher
{
    void Reset();
    List<Position> FindResource(Position start);
}