using System;
using System.Windows.Forms;

namespace TetraSnake
{
    public partial class FormTetraSnake : Form
    {
        private readonly GameController _gameController;
        private readonly CheckboxController _checkboxController;
        private readonly GameType _gameType;

        public FormTetraSnake() => InitializeComponent();

        public FormTetraSnake(GameType type) : this()
        {
            _gameType = type;
            _checkboxController = new CheckboxController();
            _checkboxController.InitializeCheckboxes(checkBoxLevel1, checkBoxLevel2, checkBoxLevel3);

            _gameController = new GameController(timer, timerSnake, pictureBoxFigures, pictureBoxGameTetraSnake, _checkboxController);
            _gameController.CloseFormAction += ExitForm;

            Start();
        }

        private void Start()
        {
            _gameController.StartGame(_gameType);
            timerScore.Interval = 1;
            timerScore.Start();

            if (_gameType == GameType.TetraSnake)
            {
                Text = "TetraSnake";
                checkBoxDeathTetramino.Visible = true;
                checkBoxDeathTetramino.Checked = _gameController.IsDeadlyTetramino;
            }

            if (_gameType == GameType.Snake || _gameType == GameType.TetraSnake)
            {
                _gameController.SnakeGame.Spawn(_gameController.GameField);
                if (_gameType == GameType.Snake) Text = "Snake";
                timerSnake.Interval = _gameController.SnakeGame.TimeUpdate;
                timerSnake.Start();
            }

            if (_gameType == GameType.Tetris || _gameType == GameType.TetraSnake)
            {
                if (_gameType == GameType.Tetris) Text = "Tetris";
                checkBoxAddTetramino.Visible = true;
                checkBoxAddTetramino.Checked = _gameController.UseExtraTetramino;
                if (_gameController.UseExtraTetramino)
                    _gameController.TetrisGame.Tetraminos = StoreTetramino.MultitudeFigures;
                timer.Interval = _gameController.TetrisGame.TimeUpdate;
                timer.Start();
            }
        }

        private void TimerSnake_Tick(object sender, EventArgs e)
        {
            _gameController.SnakeGame.Update();
            _gameController.RenderGame();
        }

        private void timer_Tick(object sender, EventArgs e) =>
            _gameController.TetrisGame.Update();

        private void timerScore_Tick(object sender, EventArgs e)
        {
            if (_gameController.TryUpdateRecord())
            {
                Text = $"Рекорд: {_gameController.Record}";
                labelRecord.Text = $"Рекорд:\n{_gameController.Record}";
            }
            labelScore.Text = $"Очки:\n{_gameController.Score}";
            labelScoreLine.Text = $"Убранные линии:\n{_gameController.ClearLine}";
        }

        private void splitContainer1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                timer.Interval = _gameController.TetrisGame.TimeUpdateDown;
        }

        private void splitContainer1_KeyUp(object sender, KeyEventArgs e)
        {
            if (_gameController.Tetramino.IsControlEnabled)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left: _gameController.InvokeTetrisKey(Keys.Left); break;
                    case Keys.Right: _gameController.InvokeTetrisKey(Keys.Right); break;
                    case Keys.Up: _gameController.InvokeTetrisKey(Keys.Up); break;
                    case Keys.Down: timer.Interval = _gameController.TetrisGame.TimeUpdate; break;
                }
            }

            switch (e.KeyCode)
            {
                case Keys.W: _gameController.InvokeSnakeKey(Keys.W); break;
                case Keys.S: _gameController.InvokeSnakeKey(Keys.S); break;
                case Keys.A: _gameController.InvokeSnakeKey(Keys.A); break;
                case Keys.D: _gameController.InvokeSnakeKey(Keys.D); break;
            }
        }

        private void ChangeCheckBox(CheckBox _, int index)
        {
            RemoveFocus();
            _gameController.ResetGameState();
            _checkboxController.SelectCheckbox(index);
            _gameController.SetLevelParameters(index + 1);
            StartTimer();
        }

        private void checkBoxLevel1_Click(object sender, EventArgs e) => ChangeCheckBox(checkBoxLevel1, 0);
        private void checkBoxLevel2_Click(object sender, EventArgs e) => ChangeCheckBox(checkBoxLevel2, 1);
        private void checkBoxLevel3_Click(object sender, EventArgs e) => ChangeCheckBox(checkBoxLevel3, 2);

        private void checkBoxDeathTetramino_Click(object sender, EventArgs e)
        {
            _gameController.EnableDeadlyTetramino(checkBoxDeathTetramino.Checked);
            _gameController.ResetGameState();
            StartTimer();
            RemoveFocus();
        }

        private void checkBoxAddTetramino_CheckedChanged(object sender, EventArgs e)
        {
            RemoveFocus();
            _gameController.UseExtraTetramino = checkBoxAddTetramino.Checked;
            _gameController.TetrisGame.Tetraminos = _gameController.UseExtraTetramino
                ? StoreTetramino.MultitudeFigures
                : StoreTetramino.Tetramino;
        }

        private void buttonExit_Click(object sender, EventArgs e) => ExitForm();

        private void StartTimer()
        {
            timer.Interval = _gameController.TetrisGame.TimeUpdate;
            timerSnake.Interval = _gameController.SnakeGame.TimeUpdate;
            if (_gameController.SnakeGame.IsStarted) timerSnake.Start();
            if (_gameController.TetrisGame.IsStarted) timer.Start();
        }

        private void RemoveFocus()
        {
            buttonExit.TabStop = false;
            ActiveForm.ActiveControl = null;
        }

        private void ExitForm()
        {
            timerScore.Stop();
            _gameController.ResetGameState();
            _gameController.SnakeGame.IsStarted = false;
            _gameController.TetrisGame.IsStarted = false;
            new FormMainMenu().Show();
            Close();
        }
    }
}
