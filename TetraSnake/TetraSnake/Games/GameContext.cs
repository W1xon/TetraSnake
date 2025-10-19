using System;
using System.Windows.Forms;

namespace TetraSnake
{
    public class GameContext
    {
        public Field ShapePreview { get; set; }
        public Field GameField { get; set; }
        public Tetramino Tetramino { get; set; }
        public Snake SnakeGame { get; private set; }
        public Tetris TetrisGame { get; private set; }

        private readonly GameController _controller;

        public GameContext(GameController controller)
        {
            _controller = controller;
        }

        public void StartSnake()
        {
            InitializeGames();
            SnakeGame.IsStarted = true;
            InitLevel();
        }

        public void StartTetris()
        {
            InitializeGames();
            TetrisGame.IsStarted = true;
            InitLevel();
        }

        public void StartTetraSnake()
        {
            InitializeGames();
            SnakeGame.IsStarted = true;
            TetrisGame.IsStarted = true;
            InitLevel();
        }

        private void InitializeGames()
        {
            SnakeGame = new Snake(_controller);
            TetrisGame = new Tetris(_controller);

            SnakeGame.GameOver += HandleGameReset;
            TetrisGame.GameOver += HandleGameReset;
        }

        private void HandleGameReset()
        {
            if (SnakeGame.IsStarted && TetrisGame.IsStarted)
            {
                ResetSnake();
                ResetTetris();
                Records.SaveTetraSnake(_controller.CheckboxController.GetCheckedIndex(), _controller.Record);
                return;
            }

            if (SnakeGame.IsStarted)
            {
                ResetSnake();
                Records.SaveSnake(_controller.CheckboxController.GetCheckedIndex(), _controller.Record);
                return;
            }

            if (TetrisGame.IsStarted)
            {
                ResetTetris();
                Records.SaveTetris(_controller.CheckboxController.GetCheckedIndex(), _controller.Record);
            }
        }

        private void ResetSnake()
        {
            SaveLevel();
            _controller.Score = 0;
            GameField.Clear();
            SnakeGame.Spawn(GameField);
        }

        private void ResetTetris()
        {
            SaveLevel();
            _controller.Score = 0;
            GameField.Clear();
            Tetramino.Spawn(_controller.GameField);
        }

        public void ResetGameState() => HandleGameReset();

        private void InitLevel()
        {
            if (SnakeGame.IsStarted)
            {
                SnakeGame.SetLevelParameters();
                _controller.CheckboxController.SelectCheckbox(SnakeGame.Level - 1);
            }

            if (TetrisGame.IsStarted)
            {
                TetrisGame.SetLevelParameters();
                _controller.CheckboxController.SelectCheckbox(TetrisGame.Level - 1);
            }
        }

        public void SaveLevel()
        {
            int lvl = _controller.CheckboxController.GetCheckedLevel();
            if (SnakeGame.IsStarted)
                SnakeGame.Level = lvl;
            if (TetrisGame.IsStarted)
                TetrisGame.Level = lvl;
        }

        public void SetLevelParameters(int lvl)
        {
            SnakeGame.Level = lvl;
            TetrisGame.Level = lvl;
            SnakeGame.SetLevelParameters(lvl);
            TetrisGame.SetLevelParameters(lvl);
        }

        public void ResumeTimers(Timer tetrisTimer, Timer snakeTimer)
        {
            if (TetrisGame.IsStarted)
                tetrisTimer.Start();
            if (SnakeGame.IsStarted)
                snakeTimer.Start();
        }
    }
}
