using System;

namespace TetraSnake
{
    public static class Apple
    {
        private static Random random = new Random();
        private static int Y = 5;
        private static int X = 0;


        public static void Spawn(Field field)
        {
            int[,] arrayField = field.Get();
            arrayField[Y, X] = 0;
            do
            {
                Y = random.Next(arrayField.GetLength(0));
                X = random.Next(arrayField.GetLength(1));
            }
            while (arrayField[Y, X] == 1 || arrayField[Y, X] == 3);
        }
        public static void Draw(Field field)
        {
            int[,] arrayField = field.Get();
            arrayField[Y, X] = 2;
        }
    }
}
