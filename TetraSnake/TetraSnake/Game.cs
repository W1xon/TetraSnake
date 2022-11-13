using System;
using System.Windows.Forms;

namespace TetraSnake
{
    internal class Game
    {
        public static string NameForm;

        public static int BlockSizeFutureFigure = 25;

        public static bool DeathTetris;
        public static bool AddTetramino;

        public static int timeUpdateSnake = 100;
        public static int timeUpdateTetris = 400;
        public static int timeUpdateDown = 50;

        public static int levelSnake = 1;
        public static int levelTetris = 1;
        public static int levelTetraSnake = 1;

        public static int[] RecordScoreTetris = new int[3];
        public static int[] RecordScoreTetraSnake = new int[3];
        public static int[] RecordScoreSnake = new int[3];


        public static bool IsStartedSnake;
        public static bool IsStartedTetris;

        public static Field fieldImage;
        public static Field gameField;
        public static Figure figure;

        public static CheckBox[] ArrayCheckBoxLevel = new CheckBox[3];
        public static void CheckBoxArraySet(params CheckBox[] CheckBox)
        {
            for (int i = 0; i < CheckBox.Length; i++)
            {
                if (ArrayCheckBoxLevel[0] == null)
                    CheckBox[0].Checked = true;
                ArrayCheckBoxLevel[i] = CheckBox[i];
            }
            GetLevel();
        }
        public static void SetLevelParameters(int level)
        {
            switch (level)
            {
                case 1:
                    if (IsStartedTetris && IsStartedSnake)
                    {
                        timeUpdateSnake = 100;
                        timeUpdateTetris = 400;
                        timeUpdateDown = 50;
                    }
                    else if (IsStartedSnake)
                        timeUpdateSnake = 100;
                    else if (IsStartedTetris)
                    {
                        timeUpdateTetris = 400;
                        timeUpdateDown = 50;
                    }
                    break;

                case 2:
                    if (IsStartedTetris && IsStartedSnake)
                    {
                        timeUpdateSnake = 70;
                        timeUpdateTetris = 200;
                        timeUpdateDown = 30;
                    }
                    else if (IsStartedSnake)
                        timeUpdateSnake = 70;
                    else if (IsStartedTetris) 
                    {

                        timeUpdateTetris = 200;
                        timeUpdateDown = 30;
                    }
                    break;
                case 3:
                    if (IsStartedTetris && IsStartedSnake)
                    {
                        timeUpdateSnake = 40;
                        timeUpdateTetris = 100;
                        timeUpdateDown = 10;
                    }
                    else if (IsStartedSnake)
                        timeUpdateSnake = 40;
                    else if (IsStartedTetris)
                    {
                        timeUpdateTetris = 100;
                        timeUpdateDown = 10;
                    }
                        break;
            }
        }
        public static void GetLevel()
        {
            if (IsStartedSnake && IsStartedTetris)
            {
                SetLevelParameters(levelTetraSnake);
                ArrayCheckBoxLevel[levelTetraSnake - 1].Checked = true;
            }
            else if (IsStartedSnake)
            {
                SetLevelParameters(levelSnake);
                ArrayCheckBoxLevel[levelSnake - 1].Checked = true;
            }
            else if (IsStartedTetris)
            {
                SetLevelParameters(levelTetris);
                ArrayCheckBoxLevel[levelTetris - 1].Checked = true;
            }
        }
        public static void SaveLevel()
        {
            int lvl = 0;
            for (int i = 0; i < ArrayCheckBoxLevel.Length; i++)
            {
                if (ArrayCheckBoxLevel[i].Checked)
                {
                    lvl = i + 1;
                    break;
                }
            }

            if (IsStartedSnake && IsStartedTetris)
                levelTetraSnake = lvl;
            else if (IsStartedSnake)
                levelSnake = lvl;
            else if (IsStartedTetris)
                levelTetris = lvl;
        }
        private static void SaveRecord()
        {
            if (IsStartedSnake && IsStartedTetris)
            {
                for (int i = 0; i < ArrayCheckBoxLevel.Length; i++)
                {
                    if (ArrayCheckBoxLevel[i].Checked)
                    {
                        RecordScoreTetraSnake[i] = FormTetraSnake.record;
                    }
                }
            }

            else if (IsStartedSnake)
            {
                for (int i = 0; i < ArrayCheckBoxLevel.Length; i++)
                {
                    if (ArrayCheckBoxLevel[i].Checked)
                    {
                        RecordScoreSnake[i] = FormTetraSnake.record;
                    }
                }
            }

            else if (IsStartedTetris)
            {
                for (int i = 0; i < ArrayCheckBoxLevel.Length; i++)
                {
                    if (ArrayCheckBoxLevel[i].Checked)
                    {
                        RecordScoreTetris[i] = FormTetraSnake.record;
                    }
                }
            }

        }
        public static int DataRecordGet()
        {

            if (IsStartedSnake && IsStartedTetris)
            {
                for (int i = 0; i < ArrayCheckBoxLevel.Length; i++)
                {
                    if (ArrayCheckBoxLevel[i].Checked)
                    {
                        return RecordScoreTetraSnake[i];
                    }
                }
            }

            else if (IsStartedSnake)
            {
                for (int i = 0; i < ArrayCheckBoxLevel.Length; i++)
                {
                    if (ArrayCheckBoxLevel[i].Checked)
                    {
                        return RecordScoreSnake[i];
                    }
                }
            }

            else if (IsStartedTetris)
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
        public static void DataReset(Field field, Timer timer1, Timer timer2, Figure objFigure)
        {
            SaveLevel();
            SaveRecord();
            int[,] arrField = field.Get();
            timer1.Stop();
            timer2.Stop();
            if (IsStartedSnake)
                LogicSnake.Death(field);
            LogicSnake.isDeath = false;
            Field.score = 0;
            Field.clearLine = 0;
            Array.Clear(arrField, 0, arrField.Length);
            objFigure.Spawn(field);
            LogicSnake.SpawnSnake(field);
        }
        private static void ShowMenuRestart(Timer timer1, Timer timer2)
        {
            DialogResult result = MessageBox.Show("Хотите повторить ?", "Вы проиграли.", MessageBoxButtons.RetryCancel, MessageBoxIcon.None);
            if (result == DialogResult.Cancel)
            {
                FormMainMenu.frmTS.ExitForm(Form.ActiveForm);
            }
            else
            {
                if (IsStartedTetris)
                {
                    timer1.Interval = timeUpdateTetris;
                    timer1.Start();
                }
                if (IsStartedSnake)
                    timer2.Start();
            }
        }
        public static void RestartGame(Field field, Figure objFigure, Timer timer1, Timer timer2)
        {
            int[,] figure = objFigure.arrayFigure;
            int[,] arrayField = field.Get();
            //проверяем змейку
            if (LogicSnake.isDeath)
            {
                DataReset(field, timer1, timer2, objFigure);
                ShowMenuRestart(timer1, timer2);
            }
            //проверяем тетрис
            for (int x = 0; x < arrayField.GetLength(1) && IsStartedTetris; x++)
            {
                if (arrayField[1, x] != 1)
                    continue;
                for (int figureX = 0; figureX < figure.GetLength(1); figureX++)
                {
                    if (figure[0, figureX] == 0)
                        continue;
                    if (x == objFigure.X + figureX)
                        break;
                    if (figureX == figure.GetLength(1) - 1)
                    {
                        DataReset(field, timer1, timer2, objFigure);
                        ShowMenuRestart(timer1, timer2);
                    }
                }
            }
        }
    }
}
