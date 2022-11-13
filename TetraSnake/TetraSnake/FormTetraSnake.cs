using System;
using System.Windows.Forms;

namespace TetraSnake
{
    public partial class FormTetraSnake : Form
    {
        public static int record;
        public FormTetraSnake()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Game.CheckBoxArraySet(checkBoxLevel1, checkBoxLevel2, checkBoxLevel3);

            timerScore.Interval = 1;
            timerScore.Start();

            Game.fieldImage = new Field();
            Game.gameField = new Field();

            Game.gameField.Set(pictureBoxGameTetraSnake, scale: 20);
            Game.fieldImage.Set(pictureBoxFigures, new Figure(Figure.Figures[0]).arrayFigure);
            Game.figure = new Figure(Figure.Figures[0], Game.gameField);

            record = Game.DataRecordGet();

            if (Game.IsStartedSnake && Game.IsStartedTetris)
            {
                Text = Game.NameForm = "TetraSnake";
                LogicSnake.DeathTetris = Game.DeathTetris;
                checkBoxDeathTetramino.Visible = true;
                checkBoxDeathTetramino.Checked = LogicSnake.DeathTetris;
            }

            if (Game.IsStartedSnake)
            {
                LogicSnake.SpawnSnake(Game.gameField);
                Text = Game.NameForm = "Snake";
                timerSnake.Interval = Game.timeUpdateSnake;
                timerSnake.Start();
            }

            if (Game.IsStartedTetris)
            {

                Text = Game.NameForm = "Tetris";
                timer.Interval = Game.timeUpdateTetris;
                timer.Start();
                if (Game.AddTetramino)
                    Figure.Figures = ListOfFigure.MultitudeFigures;
                checkBoxAddTetramino.Visible = true;
                checkBoxAddTetramino.Checked = Game.AddTetramino;
            }

        }
        private void TimerSnake_Tick(object sender, EventArgs e)
        {
            LogicSnake.Move(Game.gameField, Game.figure);
            Game.RestartGame(Game.gameField, Game.figure, timer, timerSnake);
            Apple.Draw(Game.gameField);
            DrawGame.Draw(Game.gameField.Get(), pictureBoxGameTetraSnake, pictureBoxGameTetraSnake.Height / Game.gameField.Get().GetLength(0));
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if(!Game.IsStartedSnake)
                Game.RestartGame(Game.gameField, Game.figure, timer, timerSnake);
            //отрисовываем поле с будущей фигурой
            Game.fieldImage.Set(pictureBoxFigures, new Figure(Figure.Figures[Game.figure.nextFigure >= Figure.Figures.Count ? Figure.Figures.Count -1 : Game.figure.nextFigure]).arrayFigure);
            DrawGame.Draw(Game.fieldImage.Get(), pictureBoxFigures, Game.BlockSizeFutureFigure);
            //двигаем фигуру 
            Game.gameField.Set(Game.figure.Move(Game.gameField));
            //отрисовываем игровое поле
            if(!Game.IsStartedSnake)
                DrawGame.Draw(Game.gameField.Get(), pictureBoxGameTetraSnake, pictureBoxGameTetraSnake.Height / Game.gameField.Get().GetLength(0));
        }

        public void ExitForm(Form frmTS)
        {
            timerScore.Stop();
            Game.DataReset(Game.gameField, timer, timerSnake, Game.figure);
            record = 0;
            Game.IsStartedSnake = false;
            Game.IsStartedTetris = false;
            FormMainMenu frmMM = new FormMainMenu();
            frmMM.Show();
            frmTS.Close();
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            ExitForm(ActiveForm);
        }

        private void timerScore_Tick(object sender, EventArgs e)
        {
            if (record < Field.score)
                record = Field.score;
            Text = $"{Game.NameForm}     Рекорд: {record}";
            labelRecord.Text = "Рекорд:\n" + record;
            labelScore.Text = $"Кол-во очков: \n{Field.score}";
            labelScoreLine.Text = $"Кол-во \nубранных линий: \n{Field.clearLine}";
        }


        private void splitContainer1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    timer.Interval = Game.timeUpdateDown;
                    break;
                case Keys.Z:
                    LogicSnake.Append();
                    break;
            }
           
        }
        private void splitContainer1_KeyUp(object sender, KeyEventArgs e)
        {
            if (Game.figure.pressButton == true)
            {
                //управление тетрисом
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        Game.figure.vectorX = -1;
                        break;
                    case Keys.Right:
                        Game.figure.vectorX = 1;
                        break;
                    case Keys.Down:
                        timer.Interval = Game.timeUpdateTetris;
                        break;
                    case Keys.Up:
                        Game.figure.rotate = true;
                        break;
                }

                // управление змейкой
                switch (e.KeyCode)
                {
                    case Keys.W:
                        if (LogicSnake.vectorY != 1)
                        {
                            LogicSnake.vectorX = 0;
                            LogicSnake.vectorY = -1;
                        }
                        break;
                    case Keys.S:
                        if (LogicSnake.vectorY != -1)
                        {
                            LogicSnake.vectorX = 0;
                            LogicSnake.vectorY = 1;
                        }
                        break;
                    case Keys.A:
                        if (LogicSnake.vectorX != 1)
                        {
                            LogicSnake.vectorX = -1;
                            LogicSnake.vectorY = 0;
                        }
                        break;
                    case Keys.D:
                        if (LogicSnake.vectorX != -1)
                        {
                            LogicSnake.vectorX = 1;
                            LogicSnake.vectorY = 0;
                        }
                        break;

                }
            }
        }


        private void ChangeCheckBox(CheckBox checkBox, int index)
        {
            int indexTrue = 0;
            for (int i = 0; i < Game.ArrayCheckBoxLevel.Length; i++)
            {
                if (Game.ArrayCheckBoxLevel[i].Checked && (i != index))
                    indexTrue = i;
                Game.ArrayCheckBoxLevel[i].Checked = false;
            }
            Game.ArrayCheckBoxLevel[indexTrue].Checked = true;

            Game.DataReset(Game.gameField, timer, timerSnake, Game.figure);
            Game.ArrayCheckBoxLevel[indexTrue].Checked = false;


            checkBox.Checked = true;
            Game.SetLevelParameters(index + 1);
            StartTimer();
            record = Game.DataRecordGet();

            //убираем фокус с кнопки чтобы реализовать падение тетрамино
            buttonExit.TabStop = false;
            ActiveForm.ActiveControl = null;
        }
        private void checkBoxLevel1_Click(object sender, EventArgs e)
        {
            ChangeCheckBox(checkBoxLevel1, 0);
        }

        private void checkBoxLevel2_Click(object sender, EventArgs e)
        {
            ChangeCheckBox(checkBoxLevel2, 1);
        }

        private void checkBoxLevel3_Click(object sender, EventArgs e)
        {
            ChangeCheckBox(checkBoxLevel3, 2);
        }

        private void StartTimer()
        {
            timer.Interval = Game.timeUpdateTetris;
            timerSnake.Interval = Game.timeUpdateSnake;
            if (Game.IsStartedSnake)
                timerSnake.Start();
            if (Game.IsStartedTetris)
                timer.Start();
        }

        private void checkBoxDeathTetramino_Click(object sender, EventArgs e)
        {
            Game.DeathTetris = checkBoxDeathTetramino.Checked;
            LogicSnake.DeathTetris = checkBoxDeathTetramino.Checked;
            Game.DataReset(Game.gameField, timer, timerSnake, Game.figure);
            StartTimer();

            //убираем фокус с кнопки чтобы реализовать падение тетрамино
            buttonExit.TabStop = false;
            ActiveForm.ActiveControl = null;
        }

        private void checkBoxAddTetramino_CheckedChanged(object sender, EventArgs e)
        {
            //убираем фокус с кнопки чтобы реализовать падение тетрамино
            buttonExit.TabStop = false;
            ActiveForm.ActiveControl = null;
            splitContainer1.Focus();

            Game.AddTetramino = checkBoxAddTetramino.Checked;
            if (Game.AddTetramino)
                Figure.Figures = ListOfFigure.MultitudeFigures;
            else
                Figure.Figures = ListOfFigure.Tetramino;
        }
    }
}
