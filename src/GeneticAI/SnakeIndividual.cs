using System;
using GeneticAlgorithm.Individuals;
using SnakeGame;
using SnakeGame.Math2D;
using SnakeGame.Player;

namespace GeneticGame
{
    public class SnakeIndividual : NeuralIndividual, IPlayer
    {
        public SnakeIndividual(int inputDataSize) : base(inputDataSize) { }

        private SnakeIndividual(Individual individual) : base(individual) { }

        public Vector2D GetMovingDirection(float[] data)
        {
            var networkOutput = ProcessNetwork(data);
            var max = Single.MinValue;
            var maxIndex = 0;
            for (int i = 0; i < networkOutput.Length; i++)
            {
                if (networkOutput[i] > max)
                {
                    max = networkOutput[i];
                    maxIndex = i;
                }
            }
            var direction = (Direction) maxIndex;
            return direction.GetVector();
        }

        public override Individual GetChild(float[] genotype)
        {
            return new SnakeIndividual(this)
            {
                Genotype = genotype
            };
        }
    }
}