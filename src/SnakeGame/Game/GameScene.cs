using System;
using SnakeGame.GameObjects;
using SnakeGame.Math2D;

namespace SnakeGame.Game
{
    public class GameScene
    {
        private Snake _snake;
        private Food _food;

        private readonly IObjectSpawner _objectSpawner;
        private readonly GameSettings _settings;

        public GameState State => new()
        {
            SnakeLength = _snake.Length,
            Points = _snake.FoodEaten,
            HeadPosition = _snake.HeadPosition,
            BodyPositions = _snake.BodyPositions,
            FoodPosition = _food.Position
        };

        public GameScene(GameSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _objectSpawner = new ObjectSpawner(settings.BoardSize);
        }

        public void Initialize()
        {
            _snake = _objectSpawner.SpawnSnake(_settings.InitialSnakeLength);
            _food = _objectSpawner.SpawnFood();
        }

        public MoveResult MoveSnake(Vector2D direction)
        {
            _snake.Move(direction);
            var isFoodEaten = TryToEatFood();
            return new MoveResult
            {
                IsFoodEaten = isFoodEaten
            };
        }

        private bool TryToEatFood()
        {
            if (!_snake.HeadPosition.Equals(_food.Position)) return false;
            _snake.EatFood();
            _food = _objectSpawner.SpawnFood();
            return true;
        }
        
    }
}
