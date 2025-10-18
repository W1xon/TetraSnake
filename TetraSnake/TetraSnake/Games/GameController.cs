using System.Windows.Forms;

namespace TetraSnake
{
    public class GameController
    {
        private const string GameOverTitle = "Вы проиграли.";
        private const string RetryQuestion = "Хотите повторить?";
        private const MessageBoxIcon DefaultIcon = MessageBoxIcon.None;
        
        public Field ShapePreview;
        public Field GameField;
        public Shape Shape;
        
        public bool EnableDeadlyTetramino;
        public bool UseExtraTetramino;

        public Snake SnakeGame;
        public Tetris TetrisGame;
        
        public PictureBox PBoxGame;
        public PictureBox PBoxPreview;
        
        private Timer _timer;
        private Timer _timerSnake;
        private int _levelTetraSnake = 1;
        public void InitTimer(Timer timer, Timer timerSnake)
        {
            _timer = timer;
            _timerSnake = timerSnake;
        }

        public void InitPictureBox(PictureBox preview, PictureBox game)
        {
            PBoxGame = game;
            PBoxPreview = preview;
        }
        public void StartSnakeGame()
        {
            CreateGames();
            SnakeGame.IsStarted = true;
        }
        public void StartTetrisGame()
        {
            CreateGames();
            TetrisGame.IsStarted = true;
        }
        public void StartTetraSnakeGame()
        {
            CreateGames();
            TetrisGame.IsStarted = true;
            SnakeGame.IsStarted = true;
        }

        private void CreateGames()
        {
            SnakeGame = new Snake(this);
            TetrisGame = new Tetris(this);
        }
        public void InitLevel()
        {
            if (SnakeGame.IsStarted && TetrisGame.IsStarted)
            {
                SnakeGame.SetLevelParameters();
                TetrisGame.SetLevelParameters();
                Records.ArrayCheckBoxLevel[_levelTetraSnake - 1].Checked = true;
            }
            else if (SnakeGame.IsStarted)
            {
                SnakeGame.SetLevelParameters();
                Records.ArrayCheckBoxLevel[SnakeGame.Level - 1].Checked = true;
            }
            else if (TetrisGame.IsStarted)
            {
                TetrisGame.SetLevelParameters();
                Records.ArrayCheckBoxLevel[TetrisGame.Level - 1].Checked = true;
            }
        }

        public void SaveLevel()
        {
            int lvl = Records.GetCurrentLevel();

            if (SnakeGame.IsStarted && TetrisGame.IsStarted)
                _levelTetraSnake = lvl;
            else if (SnakeGame.IsStarted)
                SnakeGame.Level = lvl;
            else if (TetrisGame.IsStarted)
                TetrisGame.Level = lvl;
        }

        
        
        public void ShowMenuRestart()
        {
            _timer.Stop();
            _timerSnake.Stop();
            DialogResult result = MessageBox.Show(
                RetryQuestion,
                GameOverTitle,
                MessageBoxButtons.RetryCancel,
                DefaultIcon
            );
            if (result == DialogResult.Cancel)
                FormMainMenu.formTetraSnake.ExitForm(Form.ActiveForm);
            else
            {
                if (TetrisGame.IsStarted)
                    _timer.Start();
                if (SnakeGame.IsStarted)
                    _timerSnake.Start();
            }
        }
        
        public bool TryRestart()
        {
            if (SnakeGame.IsStarted && LogicSnake.isDeath)
            {
                SnakeGame.Reset();
                if(TetrisGame.IsStarted)
                    TetrisGame.Reset(false);
                return true;
            }
            return false;
        }

        public void DataReset()
        {
            if(SnakeGame.IsStarted)
                SnakeGame.DataReset();
            if(TetrisGame.IsStarted) 
                TetrisGame.DataReset();
        }

        public void SetLevelParameters(int lvl)
        {
            SnakeGame.Level = lvl;
            TetrisGame.Level = lvl;
            SnakeGame.SetLevelParameters(lvl);
            TetrisGame.SetLevelParameters(lvl);
        }
    }
}
