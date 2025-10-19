using System;

namespace TetraSnake
{
    public  class Apple
    {
        private readonly Random _random = new Random();
        private Vector _position = new Vector(0,5);

        public Apple(Field field)
        {
            Vector size = field.Size; 
            do
            {
                _position = new Vector(
                    x: _random.Next(size.X),
                    y: _random.Next(size.Y)
                );
            }
            while (!field.IsFree(_position) || !field.IsInside(_position));

            Draw(field);
        }


        public void Draw(Field field)
        {
            field.SetPoint(_position, CellType.Apple);
        }
    }
}
