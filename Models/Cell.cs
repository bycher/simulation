namespace Simulation.Models;

public class Cell
{
    public Rock? Rock { get; set; }
    public Tree? Tree { get; set; }
    public Grass? Grass { get; set; }
    public Herbivore? Herbivore { get; set; }
    public Predator? Predator {  get; set; }
}