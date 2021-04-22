using System.Collections.Generic;
using GeneticAlgorithm.Individuals;

namespace GeneticAlgorithm.Operators.Crossover
{
    public interface ICrossoverOperator
    {
        List<Individual> ReproduceOffspring(List<(Individual, Individual)> parents, int childrenCount);
    }
}
