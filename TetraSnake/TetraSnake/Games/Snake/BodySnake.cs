using System;
namespace TetraSnake
{
    internal class BodySnake
    {
        public Vector Position;
        private readonly Random _random = new Random();
        public BodySnake(Vector vector)
        {
            Position = vector;
        }
        public BodySnake(Field field)
        {
            int minValueY = 10;

            int x = _random.Next(field.Size.X);
            int y = _random.Next(minValueY, field.Size.Y);

            Position = new Vector(x, y);
        }
    }
}
