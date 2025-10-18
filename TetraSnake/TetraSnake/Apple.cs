using System;

namespace TetraSnake
{
    public static class Apple
    {
        private static readonly Random _random = new Random();
        private static Vector _position = new Vector(0,5);


        public static void Spawn(Field field)
        {
            Vector size = field.Size; 
            field.SetPoint(_position, CellType.Empty);
            do
            {
                _position.Y = _random.Next(size.Y);
                _position.X = _random.Next(size.X);
            }
            while (field.IsFreeForApple(_position));
        }
        public static void Draw(Field field)
        {
            field.SetPoint(_position, CellType.Apple);
        }
    }
}
