namespace GeneticAlgorithm.Population
{
    public class PopulationSettings
    {
        public int GenerationsCount { get; set; }
        public int PopulationSize { get; set; }
        public int MatingPoolSize { get; set; }
        public int OffspringSize { get; set; }
        public float MutationRate { get; set; }

        public PopulationSettings()
        {
            GenerationsCount = 500;
            PopulationSize = 50;
            MatingPoolSize = 20;
            OffspringSize = 10;
            MutationRate = 0.02f;
        }
    }
}