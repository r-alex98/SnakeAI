using System.Collections.Generic;
using GeneticAlgorithm.Individuals;

namespace GeneticAlgorithm.Operators.Selection
{
    public interface ISelectionOperator
    {
        List<(Individual, Individual)> SelectParents(List<Individual> individuals, int parentsCount);
    }
}
