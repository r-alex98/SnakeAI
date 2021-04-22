using System.Threading.Tasks;
using GeneticAlgorithm.FitnessFunctions;
using GeneticAlgorithm.Individuals;
using SnakeGame;
using SnakeGame.Game;

namespace GeneticGame
{
    public class SnakeGameFitnessFunction : IFitnessFunction
    {
        private readonly IGameFactory _gameFactory;

        public SnakeGameFitnessFunction(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public async Task<float> EvaluateAsync(Individual individual)
        {
            return await EvaluateAsync((SnakeIndividual)individual);
        }

        private async Task<float> EvaluateAsync(SnakeIndividual individual)
        {
            var game = _gameFactory.CreateGame();
            await game.Run(individual);
            return game.Score;
        }
    }
}
