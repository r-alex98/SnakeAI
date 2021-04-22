using System.Collections.Generic;
using GeneticAlgorithm.Individuals;

namespace GeneticAlgorithm.Population
{
    public class PopulationState
    {
        public List<Individual> HighRatedIndividuals { get; set; }
        public int Generation { get; set; }
        public float HighScore { get; set; }
    }
}