using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Individuals;
using Network.NeuralMath;
using Network.NeuralMath.Cpu;

namespace GeneticAlgorithm.Operators.Selection
{
    public class SoftmaxSelectionOperator : ISelectionOperator
    {
        public List<(Individual, Individual)> SelectParents(List<Individual> individuals, int parentsCount)
        {
            var topRated = individuals
                .OrderByDescending(i => i.Score)
                .Take(parentsCount)
                .ToArray();

            var parents = new List<(Individual, Individual)>();
            for (int i = 0; i < topRated.Length; i++)
            {
                Tensor scores = new CpuTensor(new CpuStorage(Shape.ForVector(topRated.Length)));
                for (int j = 0; j < topRated.Length; j++)
                {
                    scores[j] = i != j ? topRated[j].Score : Single.Epsilon;
                }
            
                var softmaxResult = new CpuTensor();
                var maxResult = new CpuTensor();
                scores.Softmax(softmaxResult);
                softmaxResult.Max(maxResult);
                int maxIndex = (int)maxResult[1];
                
                var couple = (topRated[i], topRated[maxIndex]);
                parents.Add(couple);
            }

            return parents;
        }
    }
}
