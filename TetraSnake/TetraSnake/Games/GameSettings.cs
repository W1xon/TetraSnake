using System.Collections.Generic;

namespace TetraSnake
{
    public static class GameSettings
    {
        public static readonly Dictionary<int, int> SnakeUpdateTime = new Dictionary<int, int>()
        {
            { 1, 100 },
            { 2, 70 },
            { 3, 40 }
        };

        public static readonly Dictionary<int, (int TimeUpdate, int TimeUpdateDown)> TetrisUpdateTime = new Dictionary<int, (int TimeUpdate, int TimeUpdateDown)>()
        {
            { 1, (400, 50) },
            { 2, (200, 30) },
            { 3, (100, 10) }
        };

        public const int AppleScore = 40;
    }
}