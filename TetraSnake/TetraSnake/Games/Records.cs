namespace TetraSnake
{
    public static class Records
    {
        public static readonly int[] RecordScoreTetraSnake = new int[3];
        public static readonly int[] RecordScoreSnake = new int[3];
        public static readonly int[] RecordScoreTetris = new int[3];

        public static void SaveTetraSnake(int lvlIndex, int record) =>  RecordScoreTetraSnake[lvlIndex] =  record;
        public static void SaveSnake(int lvlIndex, int record) =>  RecordScoreSnake[lvlIndex] = record;
        public static void SaveTetris(int lvlIndex, int record) =>  RecordScoreTetris[lvlIndex] = record;
    }
}