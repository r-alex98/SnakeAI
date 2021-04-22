using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnakeGame.GameComponents;
using SnakeGame.GameOverConditions;
using SnakeGame.Player;

namespace SnakeGame.Game
{
    public class Game : ISnakeGame
    {
        private IPlayer _player;
        private readonly IList<IScoreCounter> _scoreCounters;
        private readonly IList<IDataCollector> _dataCollectors;
        private readonly IList<IGameOverCondition> _gameOverConditions;
        
        private readonly GameScene _gameScene;
        
        public float Score => _scoreCounters.Sum(counter => counter.TotalScore);

        public event Func<IPlayer, GameState, Task> GameStateUpdated;
        public event Action<MoveResult> MoveFinished;
        
        public Game(GameSettings settings, IList<IScoreCounter> scoreCounter, IList<IDataCollector> dataCollector, IList<IGameOverCondition> gameOverConditions)
        {
            _scoreCounters = scoreCounter ?? throw new ArgumentNullException(nameof(scoreCounter));
            _dataCollectors = dataCollector ?? throw new ArgumentNullException(nameof(dataCollector));
            _gameOverConditions = gameOverConditions ?? throw new ArgumentNullException(nameof(gameOverConditions));

            _gameScene = new GameScene(settings);
            
            _gameOverConditions.Add(new CollisionWithBodyCondition());
            _gameOverConditions.Add(new CollisionWithBorderCondition(settings.BoardSize));
            foreach (var condition in gameOverConditions)
            {
                if(condition is IGameEventHandler handler)
                    SubscribeEventHandler(handler);
            }
        }

        public void SubscribeEventHandler(IGameEventHandler handler)
        {
            MoveFinished += handler.OnMoveFinished;
        }
        
        private async Task OnGameStateUpdated()
        {
            if (GameStateUpdated is null)
                return;
            
            var tasks = new List<Task>();
            var callbackList = GameStateUpdated?.GetInvocationList();
            for (int i = 0; i < callbackList.Length; i++)
            {
                var func = (Func<IPlayer, GameState, Task>) callbackList[i];
                var task = func(_player, _gameScene.State);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }
        
        private void OnMoveFinished(MoveResult moveResult)
        {
            MoveFinished?.Invoke(moveResult);
        }

        private void Start()
        {
            _gameScene.Initialize();
        }
        
        public async Task Run(IPlayer player)
        {
            _player = player;
            
            Start();
            while (!IsGameOver())
            {
                await Update();
            }
        }
        
        private bool IsGameOver()
        {
            return _gameOverConditions.Any(condition => condition.IsGameOver(_gameScene.State));
        }
        
        private async Task Update()
        {
            var data = _dataCollectors.SelectMany(collector => collector.CollectData(_gameScene.State)).ToArray();
            var direction = _player.GetMovingDirection(data);
            var moveResult = _gameScene.MoveSnake(direction);
            OnMoveFinished(moveResult);

            foreach (var counter in _scoreCounters)
            {
                counter.UpdateScore(_gameScene.State);
            }
            await OnGameStateUpdated();
        }
    }
}
