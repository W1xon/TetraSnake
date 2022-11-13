using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetraSnake
{
    internal class BodySnake
    {
        Random random = new Random();
        public BodySnake(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public BodySnake(Field field)
        {
            int minValueY = 0;
            if (Game.IsStartedTetris)
                minValueY = 10;
            this.X = random.Next(field.Get().GetLength(1));
            this.Y = random.Next(minValueY, field.Get().GetLength(0));
        }
        public int X { get; set; }
        public int Y { get; set; }
        public void SetPosition(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public static bool Compare(BodySnake bodyOne, BodySnake bodyTwo)
        {
            if (bodyOne.X == bodyTwo.X && bodyOne.Y == bodyTwo.Y)
                return true;
            return false;
        }
    }
}
