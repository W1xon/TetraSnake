using System;
using System.Windows.Forms;

namespace TetraSnake
{
    public partial class FormTetraSnake : Form
    {
        public static int Record;
        private GameController _gameController;
        private string _nameForm;

        public FormTetraSnake()
        {
            InitializeComponent();
        }

        public void SetGameController(GameController controller)
        {
            _gameController = controller;
            _gameController.InitTimer(timer, timerSnake);
            _gameController.InitPictureBox(pictureBoxFigures, pictureBoxGameTetraSnake);
            Start();
        }

        private void Start()
        {
            Records.CheckBoxArraySet(checkBoxLevel1, checkBoxLevel2, checkBoxLevel3);
            _gameController.InitLevel();
            timerScore.Interval = 1;
            timerScore.Start();

            _gameController.GameField = new Field(pictureBoxGameTetraSnake.Size, scale: 20);
            _gameController.ShapePreview = new Field(pictureBoxFigures.Size, Shape.Figures[0]);
            _gameController.Shape = new Shape(Shape.Figures[0], _gameController.GameField);
            _gameController.Shape.OnSpawnBlocked += _gameController.TetrisGame.Reset;

            Record = Records.DataRecordGet(_gameController);

            if (_gameController.SnakeGame.IsStarted && _gameController.TetrisGame.IsStarted)
            {
                Text = _nameForm = "TetraSnake";
                LogicSnake.DeathTetris = _gameController.EnableDeadlyTetramino;
                checkBoxDeathTetramino.Visible = true;
                checkBoxDeathTetramino.Checked = LogicSnake.DeathTetris;
            }

            if (_gameController.SnakeGame.IsStarted)
            {
                LogicSnake.SpawnSnake(_gameController.GameField);
                Text = _nameForm = "Snake";
                timerSnake.Interval = _gameController.SnakeGame.TimeUpdate;
                timerSnake.Start();
            }

            if (_gameController.TetrisGame.IsStarted)
            {
                Text = _nameForm = "Tetris";
                timer.Interval = _gameController.TetrisGame.TimeUpdate;
                timer.Start();

                if (_gameController.UseExtraTetramino)
                    Shape.Figures = ListOfFigure.MultitudeFigures;

                checkBoxAddTetramino.Visible = true;
                checkBoxAddTetramino.Checked = _gameController.UseExtraTetramino;
            }
        }

        #region Игровые таймеры
        private void TimerSnake_Tick(object sender, EventArgs e)
        {
            _gameController.SnakeGame.Update();
            DrawGame.Draw(_gameController.GameField, pictureBoxGameTetraSnake,
                pictureBoxGameTetraSnake.Height / _gameController.GameField.Size.Y);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            _gameController.TetrisGame.Update();
        }

        private void timerScore_Tick(object sender, EventArgs e)
        {
            if (Record < Field.score)
                Record = Field.score;

            Text = $"{_nameForm}     Рекорд: {Record}";
            labelRecord.Text = "Рекорд:\n" + Record;
            labelScore.Text = $"Кол-во очков: \n{Field.score}";
            labelScoreLine.Text = $"Кол-во \nубранных линий: \n{Field.clearLine}";
        }
        #endregion

        #region Управление клавишами
        private void splitContainer1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    timer.Interval = _gameController.TetrisGame.timeUpdateDown;
                    break;
                case Keys.Z:
                    LogicSnake.Append();
                    break;
            }
        }

        private void splitContainer1_KeyUp(object sender, KeyEventArgs e)
        {
            if (_gameController.Shape.pressButton == true)
            {
                // управление тетрисом
                switch (e.KeyCode)
                {
                    case Keys.Left: _gameController.Shape.VectorX = -1; break;
                    case Keys.Right: _gameController.Shape.VectorX = 1; break;
                    case Keys.Down: timer.Interval = _gameController.TetrisGame.TimeUpdate; break;
                    case Keys.Up: _gameController.Shape.CanRotate = true; break;
                }

                // управление змейкой
                switch (e.KeyCode)
                {
                    case Keys.W:
                        if (LogicSnake.Direction.Y != 1) { LogicSnake.Direction.X = 0; LogicSnake.Direction.Y = -1; }
                        break;
                    case Keys.S:
                        if (LogicSnake.Direction.Y != -1) { LogicSnake.Direction.X = 0; LogicSnake.Direction.Y = 1; }
                        break;
                    case Keys.A:
                        if (LogicSnake.Direction.X != 1) { LogicSnake.Direction.X = -1; LogicSnake.Direction.Y = 0; }
                        break;
                    case Keys.D:
                        if (LogicSnake.Direction.X != -1) { LogicSnake.Direction.X = 1; LogicSnake.Direction.Y = 0; }
                        break;
                }
            }
        }
        #endregion

        #region UI / Чекбоксы и кнопки
        private void ChangeCheckBox(CheckBox checkBox, int index)
        {
            int indexTrue = 0;
            for (int i = 0; i < Records.ArrayCheckBoxLevel.Length; i++)
            {
                if (Records.ArrayCheckBoxLevel[i].Checked && (i != index))
                    indexTrue = i;
                Records.ArrayCheckBoxLevel[i].Checked = false;
            }

            Records.ArrayCheckBoxLevel[indexTrue].Checked = true;

            _gameController.DataReset();
            Records.ArrayCheckBoxLevel[indexTrue].Checked = false;

            checkBox.Checked = true;
            _gameController.SetLevelParameters(index + 1);
            StartTimer();
            Record = Records.DataRecordGet(_gameController);

            RemoveFocusFromButton(buttonExit);
        }

        private void checkBoxLevel1_Click(object sender, EventArgs e) => ChangeCheckBox(checkBoxLevel1, 0);
        private void checkBoxLevel2_Click(object sender, EventArgs e) => ChangeCheckBox(checkBoxLevel2, 1);
        private void checkBoxLevel3_Click(object sender, EventArgs e) => ChangeCheckBox(checkBoxLevel3, 2);

        private void checkBoxDeathTetramino_Click(object sender, EventArgs e)
        {
            _gameController.EnableDeadlyTetramino = checkBoxDeathTetramino.Checked;
            LogicSnake.DeathTetris = checkBoxDeathTetramino.Checked;
            _gameController.DataReset();
            StartTimer();
            RemoveFocusFromButton(buttonExit);
        }

        private void checkBoxAddTetramino_CheckedChanged(object sender, EventArgs e)
        {
            RemoveFocusFromButton(buttonExit);
            splitContainer1.Focus();

            _gameController.UseExtraTetramino = checkBoxAddTetramino.Checked;
            Shape.Figures = _gameController.UseExtraTetramino
                ? ListOfFigure.MultitudeFigures
                : ListOfFigure.Tetramino;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            ExitForm(ActiveForm);
        }
        #endregion

        #region Вспомогательные методы
        private void StartTimer()
        {
            timer.Interval = _gameController.TetrisGame.TimeUpdate;
            timerSnake.Interval = _gameController.SnakeGame.TimeUpdate;
            if (_gameController.SnakeGame.IsStarted) timerSnake.Start();
            if (_gameController.TetrisGame.IsStarted) timer.Start();
        }

        private void RemoveFocusFromButton(Control button)
        {
            button.TabStop = false;
            ActiveForm.ActiveControl = null;
        }

        public void ExitForm(Form frmTS)
        {
            timerScore.Stop();
            _gameController.DataReset();
            Record = 0;
            _gameController.SnakeGame.IsStarted = false;
            _gameController.TetrisGame.IsStarted = false;

            FormMainMenu frmMM = new FormMainMenu();
            frmMM.Show();
            frmTS.Close();
        }
        #endregion
    }
}
