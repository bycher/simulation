namespace Simulation.Models;

public class SimulationParams
{
    public int N { get; set; }
    public int M { get; set; }

    public int RocksNumber { get; set; }
    public int TreeNumber { get; set; }
    public int GrassNumber { get; set; }

    public int HerbivoreNumber { get; set; }
    public int HerbivoreSpeed { get; set; }
    public int HerbivoreHealth { get; set; }

    public int PredatorNumber { get; set; }
    public int PredatorSpeed { get; set; }
    public int PredatorHealth { get; set; }
    public int PredatorAttack { get; set; }
}