namespace GeneticAlgorithm.Individuals
{
    public abstract class Individual
    {
        public int Id { get; set; }
        public float Score { get; set; }
        
        public abstract float[] Genotype { get; set; }

        public abstract Individual GetChild(float[] genotype);
    }
}
