using System.Collections.Generic;
using GeneticAlgorithm.Individuals;
using Network.NeuralMath;

namespace GeneticAlgorithm.Operators.Mutation
{
    public class MutationOperator : IMutationOperator
    {
        public void Mutate(List<Individual> individuals, float mutationRate)
        {
            foreach (var individual in individuals)
            {
                var mutatedGenotype = individual.Genotype;
                for (var i = 0; i < mutatedGenotype.Length; i++)
                {
                    if (RandomUtil.GetRandomNumber(0, 1) <= mutationRate)
                    {
                        mutatedGenotype[i] = (float)RandomUtil.GetGaussian(0, 1);
                    }
                }

                individual.Genotype = mutatedGenotype;
            }
        }

    }
}
