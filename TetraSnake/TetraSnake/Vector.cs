namespace TetraSnake
{
    public struct Vector
    {
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }

        public int Y { get; set; }

        public static bool operator ==(Vector vectorA, Vector vectorB)
        {
            return vectorA.Y == vectorB.Y && vectorA.X == vectorB.X;
        }

        public static bool operator !=(Vector vectorA, Vector vectorB)
        {
            return vectorA.Y != vectorB.Y || vectorA.X != vectorB.X;
        }

        public static Vector operator +(Vector vectorA, Vector vectorB)
        {
            return new Vector
            {
                X = vectorA.X + vectorB.X,
                Y = vectorA.Y + vectorB.Y
            };
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector
            {
                X = a.X - b.X,
                Y = a.Y - b.Y
            };
        }
        public override bool Equals(object? obj) => obj is Vector v && X == v.X && Y == v.Y;

    }
}