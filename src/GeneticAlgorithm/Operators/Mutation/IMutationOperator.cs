using System.Collections.Generic;
using GeneticAlgorithm.Individuals;

namespace GeneticAlgorithm.Operators.Mutation
{
    public interface IMutationOperator
    {
        void Mutate(List<Individual> individuals, float mutationRate);
    }
}
