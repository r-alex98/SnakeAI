using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnakeGame.Game;
using SnakeGame.GameComponents;
using SnakeGame.GameOverConditions;
using SnakeGame.Player;

namespace GeneticGame
{
    public class GeneticGameFactory : IGameFactory
    {
        private readonly Func<List<IScoreCounter>> _scoreCountersFunc;
        private readonly Func<List<IDataCollector>> _dataCollectorsFunc;
        private readonly Func<List<IGameOverCondition>> _gameOverConditionsFunc;
        
        private readonly GameSettings _settings;

        private readonly Func<IPlayer, GameState, Task> _onGameStateUpdated;

        public GeneticGameFactory(Func<List<IScoreCounter>> scoreCounters, Func<List<IDataCollector>> dataCollectors, Func<List<IGameOverCondition>> gameOverConditions, GameSettings settings, Func<IPlayer, GameState, Task> onGameStateUpdated)
        {
            _scoreCountersFunc = scoreCounters ?? throw new ArgumentNullException(nameof(scoreCounters));
            _dataCollectorsFunc = dataCollectors ?? throw new ArgumentNullException(nameof(dataCollectors));
            _gameOverConditionsFunc = gameOverConditions ?? throw new ArgumentNullException(nameof(gameOverConditions));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _onGameStateUpdated = onGameStateUpdated;
        }
        
        public ISnakeGame CreateGame()
        {
            var counters = _scoreCountersFunc();
            var collectors = _dataCollectorsFunc();
            var gameOverConditions = _gameOverConditionsFunc();
            var game = new Game(_settings, counters, collectors, gameOverConditions);
            if(_onGameStateUpdated != null) game.GameStateUpdated += _onGameStateUpdated;
            return game;
        }
        
        public IPlayer CreatePlayer()
        {
            var inputDataSize = _dataCollectorsFunc().Sum(dataCollector => dataCollector.DataSize);
            return new SnakeIndividual(inputDataSize);
        }
    }
}
