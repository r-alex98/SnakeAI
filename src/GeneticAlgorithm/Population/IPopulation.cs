using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GeneticAlgorithm.FitnessFunctions;
using GeneticAlgorithm.Individuals;

namespace GeneticAlgorithm.Population
{
    public interface IPopulation
    {
        List<Individual> Individuals { get; }
        float MaxScore { get; }
        Task EvolveAsync(IFitnessFunction mainFitnessFunction, IFitnessFunction offspringFitnessFunction, CancellationToken token);
        event Func<PopulationState, Task> GenerationCompleted;  
    }
}
