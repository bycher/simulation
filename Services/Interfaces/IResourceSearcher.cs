using Simulation.Models;

namespace Simulation.Services.Interfaces;

public interface IResourceSearcher
{
    Queue<Position> FindResource(Position start);
}