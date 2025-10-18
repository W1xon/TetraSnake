namespace TetraSnake
{
    public static class ArrayExtensions
    {
        public static bool SequenceEqual2D(this int[,] first, int[,] second)
        {
            if (ReferenceEquals(first, second))
                return true;
            if (first == null || second == null)
                return false;
            if (first.GetLength(0) != second.GetLength(0) ||
                first.GetLength(1) != second.GetLength(1))
                return false;

            for (int y = 0; y < first.GetLength(0); y++)
            {
                for (int x = 0; x < first.GetLength(1); x++)
                {
                    if (first[y, x] != second[y, x])
                        return false;
                }
            }

            return true;
        }
        public static int[,] RotateMatrix(this int[,] arr)
        {
            int[,] rotated = new int[arr.GetLength(1), arr.GetLength(0)];
            for (int y = 0; y < arr.GetLength(0); y++)
            {
                for (int x = 0; x < arr.GetLength(1); x++)
                {
                    rotated[x, rotated.GetLength(1) - 1 - y] = arr[y, x];
                }
            }
            return rotated;
        }
    }

}