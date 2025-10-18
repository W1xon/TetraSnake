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
            Position.X = _random.Next(field.Size.X);
            Position.Y = _random.Next(minValueY, field.Size.Y);
        }
        public void SetPosition(Vector position)
        {
            Position = position;
        }
    }
}
