namespace TetraSnake
{
    public readonly struct Vector
    {
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public static bool operator ==(Vector a, Vector b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Vector a, Vector b) => !(a == b);

        public static Vector operator +(Vector a, Vector b) => new Vector(a.X + b.X, a.Y + b.Y);
        public static Vector operator -(Vector a, Vector b) => new Vector(a.X - b.X, a.Y - b.Y);

        public override bool Equals(object? obj) => obj is Vector v && X == v.X && Y == v.Y;

        public Vector AddX(int dx) => new Vector(X + dx, Y);
        public Vector AddY(int dy) => new Vector(X, Y + dy);
    }
}