using System;

namespace GeneticAlgorithm.Individuals
{
    public class NeuralIndividual : Individual
    {
        private readonly int _inputDataSize;
        private readonly NeuralNetworkComponent _networkComponent;
        
        public override float[] Genotype
        {
            get => _networkComponent.NeuronsValues;
            set => _networkComponent.NeuronsValues = value;
        }
        
        protected NeuralIndividual(int inputDataSize)
        {
            _inputDataSize = inputDataSize;
            _networkComponent = new NeuralNetworkComponent(_inputDataSize);
        }

        protected NeuralIndividual(Individual individual)
        {
            var neuralIndividual = (NeuralIndividual) individual ?? throw new ArgumentException(nameof(individual));
            _inputDataSize = neuralIndividual._inputDataSize;
            _networkComponent = new NeuralNetworkComponent(_inputDataSize);
        }

        protected float[] ProcessNetwork(float[] input)
        {
            return _networkComponent.Process(input);
        }
        
        public override Individual GetChild(float[] genotype)
        {
            return new NeuralIndividual(this)
            {
                Genotype = genotype
            };
        }
    }
}
