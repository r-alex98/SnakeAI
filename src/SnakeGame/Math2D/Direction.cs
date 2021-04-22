namespace SnakeGame.Math2D
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public static class DirectionExtensions
    {
        public static Vector2D GetVector(this Direction direction)
        {
            return direction switch
            {
                Direction.Up => Vector2D.UpUnitVector,
                Direction.Right => Vector2D.RightUnitVector,
                Direction.Down => Vector2D.DownUnitVector,
                Direction.Left => Vector2D.LeftUnitVector
            };
        }
    }
}