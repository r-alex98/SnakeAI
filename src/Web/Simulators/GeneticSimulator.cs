using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GeneticAlgorithm.FitnessFunctions;
using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Operators.Crossover;
using GeneticAlgorithm.Operators.Mutation;
using GeneticAlgorithm.Operators.Selection;
using GeneticAlgorithm.Population;
using GeneticGame;
using GeneticGame.GameComponents;
using GeneticGame.GameOverConditions;
using GeneticGame.Settings;
using SnakeGame;
using SnakeGame.Game;
using SnakeGame.GameComponents;
using SnakeGame.GameOverConditions;
using SnakeGame.Player;

namespace Web.Simulators
{
    public class GeneticSimulator : ISimulator
    {
        private readonly IPopulation _population;

        private readonly IFitnessFunction _populationFitnessFunction;
        private readonly IFitnessFunction _offspringFitnessFunction;

        private readonly CancellationTokenSource _tokenSource;

        public event Func<int, GameState, Task> GameStateUpdated;
        public event Func<SimulatorState, Task> IterationCompleted;
        
        public GeneticSimulator(PopulationSettings populationSettings, ScoreSettings scoreSettings, GameSettings gameSettings)
        {
            var gameFactory = CreateGameFactory(gameSettings, scoreSettings, UpdateGameBoardAsync);
            var offspringGameFactory = CreateGameFactory(gameSettings, scoreSettings, null);
            _populationFitnessFunction = new SnakeGameFitnessFunction(gameFactory);
            _offspringFitnessFunction = new SnakeGameFitnessFunction(offspringGameFactory);

            _population = new Population
            (
                populationSettings,
                new PointBasedCrossoverOperator(),
                new SoftmaxSelectionOperator(),
                new MutationOperator(), 
                () => (Individual) gameFactory.CreatePlayer()
            );
            _population.GenerationCompleted += OnGenerationFinished;

            _tokenSource = new CancellationTokenSource();
        }

        private IGameFactory CreateGameFactory(GameSettings gameSettings, ScoreSettings scoreSettings, Func<IPlayer, GameState, Task> onGameStateUpdated)
        {
            Func<List<IScoreCounter>> scoreCounters = () => new List<IScoreCounter>
            {
                new EatingScoreCounter(scoreSettings.ScoreForEating),
                new DistanceToFoodScoreCounter(scoreSettings.ScoreForCorrectWay, scoreSettings.ScoreForWrongWay)
            };
            
            Func<List<IDataCollector>> dataCollectors = () => new List<IDataCollector>
            {
                new FoodLocationDataCollector(),
                new NearestCollisionsDataCollector(gameSettings.BoardSize)
            };

            Func<List<IGameOverCondition>> gameOverConditionsFunc = () => new List<IGameOverCondition>
            {
                new MovesWithoutEatingCondition(100)
            };

            return new GeneticGameFactory(scoreCounters, dataCollectors, gameOverConditionsFunc, gameSettings, onGameStateUpdated);
        }

        private async Task OnGenerationFinished(PopulationState state)
        {
            var simulationState = new SimulatorState
            {
                Iteration = state.Generation,
                HighScore = state.HighScore,
                HighRatedIndividualsIds = state.HighRatedIndividuals.Select(ind => ind.Id).ToArray()
            };
            
            if (IterationCompleted != null)
                await IterationCompleted.Invoke(simulationState);
        }

        public async Task SimulateAsync()
        {
            await _population.EvolveAsync(_populationFitnessFunction, _offspringFitnessFunction, _tokenSource.Token);
        }

        public void StopSimulation()
        {
            _tokenSource.Cancel();
        }

        private async Task UpdateGameBoardAsync(IPlayer player, GameState state)
        {
            var id = ((Individual) player).Id;
            if (GameStateUpdated != null)
                await GameStateUpdated.Invoke(id, state);
        }
        
    }
}
