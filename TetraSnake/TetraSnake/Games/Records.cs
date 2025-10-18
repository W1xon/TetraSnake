using System.Windows.Forms;

namespace TetraSnake
{
    public static class Records
    {
        public static int[] RecordScoreTetraSnake = new int[3];
        public static int[] RecordScoreSnake = new int[3];
        public static int[] RecordScoreTetris = new int[3];
        
        public static CheckBox[] ArrayCheckBoxLevel = new CheckBox[3];
        public static int DataRecordGet(GameController controller)
        {

            if (controller.SnakeGame.IsStarted && controller.TetrisGame.IsStarted)
            {
                for (int i = 0; i < ArrayCheckBoxLevel.Length; i++)
                {
                    if (ArrayCheckBoxLevel[i].Checked)
                    {
                        return RecordScoreTetraSnake[i];
                    }
                }
            }

            else if (controller.SnakeGame.IsStarted)
            {
                for (int i = 0; i < ArrayCheckBoxLevel.Length; i++)
                {
                    if (ArrayCheckBoxLevel[i].Checked)
                    {
                        return RecordScoreSnake[i];
                    }
                }
            }

            else if (controller.TetrisGame.IsStarted)
            {
                for (int i = 0; i < ArrayCheckBoxLevel.Length; i++)
                {
                    if (ArrayCheckBoxLevel[i].Checked)
                    {
                        return RecordScoreTetris[i];
                    }
                }
            }

            return 0;
        }
        public static void SaveRecord(GameController controller)
        {
            if (controller.SnakeGame.IsStarted && controller.TetrisGame.IsStarted)
            {
                for (int i = 0; i < ArrayCheckBoxLevel.Length; i++)
                {
                    if (ArrayCheckBoxLevel[i].Checked)
                    {
                        RecordScoreTetraSnake[i] = FormTetraSnake.Record;
                    }
                }
            }

            else if (controller.SnakeGame.IsStarted)
            {
                for (int i = 0; i < ArrayCheckBoxLevel.Length; i++)
                {
                    if (ArrayCheckBoxLevel[i].Checked)
                    {
                        RecordScoreSnake[i] = FormTetraSnake.Record;
                    }
                }
            }

            else if (controller.TetrisGame.IsStarted)
            {
                for (int i = 0; i < ArrayCheckBoxLevel.Length; i++)
                {
                    if (ArrayCheckBoxLevel[i].Checked)
                    {
                        RecordScoreTetris[i] = FormTetraSnake.Record;
                    }
                }
            }

        }
        
        public static void CheckBoxArraySet(params CheckBox[] CheckBox)
        {
            for (int i = 0; i < CheckBox.Length; i++)
            {
                if (ArrayCheckBoxLevel[0] == null)
                    CheckBox[0].Checked = true;
                ArrayCheckBoxLevel[i] = CheckBox[i];
            }
        }

        public static int GetCurrentLevel()
        {
            for (int i = 0; i < ArrayCheckBoxLevel.Length; i++)
            {
                if (ArrayCheckBoxLevel[i].Checked)
                {
                    return i + 1;
                }
            }
            return 0;
        }
    }
}