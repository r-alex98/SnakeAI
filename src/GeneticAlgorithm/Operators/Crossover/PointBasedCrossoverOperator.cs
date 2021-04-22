using System;
using System.Collections.Generic;
using GeneticAlgorithm.Individuals;

namespace GeneticAlgorithm.Operators.Crossover
{
    public class PointBasedCrossoverOperator : ICrossoverOperator
    {
        public List<Individual> ReproduceOffspring(List<(Individual, Individual)> parents, int childrenCount)
        {
            var children = new List<Individual>();
            var genotypeLength = parents[0].Item1.Genotype.Length;
            var crossoverPoint = GetCrossoverPoint(genotypeLength / 2);
            for (int i = 0; i < childrenCount; i++)
            {
                var parent1 = parents[i].Item1;
                var parent2 = parents[i].Item2;
                var childGenotype = CreateChildGenotype(parent1, parent2, crossoverPoint);
                var child = parent1.GetChild(childGenotype);
                children.Add(child);
            }

            return children;
        }

        private int GetCrossoverPoint(int genotypeLength)
        {
            return genotypeLength / 2;
        }

        private float[] CreateChildGenotype(Individual parent1, Individual parent2, int crossoverPoint)
        {
            var newGenotype = new float[parent1.Genotype.Length];
            var parent1PartLength = crossoverPoint;
            var parent2PartLength = parent2.Genotype.Length - crossoverPoint;
            Array.Copy(parent1.Genotype, 0, newGenotype, 0, parent1PartLength);
            Array.Copy(parent2.Genotype, crossoverPoint, newGenotype, crossoverPoint, parent2PartLength);
            return newGenotype;
        }
    }
}
