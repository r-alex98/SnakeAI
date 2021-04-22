using System;

namespace SnakeGame.Math2D
{
    public struct Vector2D
    {
        public int X;
        public int Y;
        
        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2D UpUnitVector => (0, -1);
        public static Vector2D RightUnitVector => (1, 0);
        public static Vector2D DownUnitVector => (0, 1);
        public static Vector2D LeftUnitVector => (-1, 0);
            
        public bool Equals(Vector2D vector)
        {
            return X == vector.X && Y == vector.Y;
        }

        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            var newX = v1.X + v2.X;
            var newY = v1.Y - v2.Y;
            return new Vector2D(newX, newY);
        }
        
        public static implicit operator Vector2D(ValueTuple<int, int> points)
        {
            return new Vector2D(points.Item1, points.Item2);
        }

        public static float CalcDistance(Vector2D v1, Vector2D v2)
        {
            return MathF.Sqrt(MathF.Pow(v1.X - v2.X, 2) + MathF.Pow(v1.Y - v2.Y, 2));
        }

        public bool IsOutOfPlane(int planeSize)
        {
            return X < 0 || X > planeSize - 1 ||
                   Y < 0 || Y > planeSize - 1;
        }
    }
}
