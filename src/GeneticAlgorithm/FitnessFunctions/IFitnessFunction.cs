using System.Threading.Tasks;
using GeneticAlgorithm.Individuals;

namespace GeneticAlgorithm.FitnessFunctions
{
    public interface IFitnessFunction
    {
        Task<float> EvaluateAsync(Individual individual);
    }
}
