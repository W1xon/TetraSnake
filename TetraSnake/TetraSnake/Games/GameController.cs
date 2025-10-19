using System;
using System.Windows.Forms;

namespace TetraSnake
{
    public class GameController
    {

        public event Action<Keys> TetrisKeyPressed;
        public event Action<Keys> SnakeKeyPressed;
        public event Action CloseFormAction;

        public int Score { get; set; }
        public int ClearLine { get; set; }
        public int Record { get; set; }
        public bool IsDeadlyTetramino { get; private set; }
        public bool UseExtraTetramino { get; set; }

        public GameRenderer GameRenderer { get; }
        public PictureBox PBoxGame { get; }
        public PictureBox PBoxPreview { get; }
        public CheckboxController CheckboxController { get; }

        public Field GameField
        {
            get => _context.GameField;
            private set => _context.GameField = value;
        }

        public Field ShapePreview
        {
            get => _context.ShapePreview;
            private set => _context.ShapePreview = value;
        }

        public Tetramino Tetramino
        {
            get => _context.Tetramino;
            private set => _context.Tetramino = value;
        }

        public Snake SnakeGame => _context.SnakeGame;
        public Tetris TetrisGame => _context.TetrisGame;

        private const string GameOverTitle = "Вы проиграли.";
        private const string RetryQuestion = "Хотите повторить?";
        private const MessageBoxIcon DefaultIcon = MessageBoxIcon.None;

        private bool _isRestartDialogOpen;

        private readonly GameContext _context;
        private readonly Timer _timer;
        private readonly Timer _timerSnake;
        public GameController(Timer timer, Timer timerSnake, PictureBox preview, PictureBox game, CheckboxController checkboxController)
        {
            _timer = timer;
            _timerSnake = timerSnake;
            PBoxGame = game;
            PBoxPreview = preview;
            CheckboxController = checkboxController;
            _context = new GameContext(this);
            GameRenderer = new GameRenderer();
        }

        public void InvokeTetrisKey(Keys key) => TetrisKeyPressed?.Invoke(key);
        public void InvokeSnakeKey(Keys key) => SnakeKeyPressed?.Invoke(key);
        public void SetLevelParameters(int lvl) => _context.SetLevelParameters(lvl);
        public void ResetGameState() => _context.ResetGameState();

        public void StartGame(GameType type)
        {
            switch (type)
            {
                case GameType.Snake:
                    _context.StartSnake();
                    break;
                case GameType.Tetris:
                    _context.StartTetris();
                    break;
                case GameType.TetraSnake:
                    _context.StartTetraSnake();
                    break;
            }
            InitializeFields();
        }

        private void InitializeFields()
        {
            GameField = new Field(PBoxGame.Size, scale: 20);
            var random = new Random();
            var randomIndex = random.Next(TetrisGame.Tetraminos.Count);
            ShapePreview = new Field(PBoxPreview.Size, TetrisGame.Tetraminos[randomIndex]);
            Tetramino = new Tetramino(TetrisGame.Tetraminos[randomIndex], _context.GameField);


            Tetramino.OnSpawnBlocked += TetrisGame.Reset;
            Tetramino.GetNextTetramino += TetrisGame.GetNextTetraminoMatrix;
            GameField.ScoreChanged += i => Score += i;
            GameField.LinesCleared += i => ClearLine += i;
            SnakeGame.ScoreChanged = i => Score += i;
        }

        public bool TryUpdateRecord()
        {
            if (Score <= Record)
                return false;

            Record = Score;
            return true;
        }

        public void ShowMenuRestart()
        {
            if (_isRestartDialogOpen)
                return;

            _isRestartDialogOpen = true;
            _timer.Stop();
            _timerSnake.Stop();

            var result = MessageBox.Show(RetryQuestion, GameOverTitle, MessageBoxButtons.RetryCancel, DefaultIcon);
            _isRestartDialogOpen = false;

            if (result == DialogResult.Cancel)
                CloseFormAction?.Invoke();
            else
                _context.ResumeTimers(_timer, _timerSnake);
        }

        public void EnableDeadlyTetramino(bool enabled)
        {
            IsDeadlyTetramino = enabled;
            SnakeGame.CanDieFromTetramino = enabled;
        }

        public void RenderPreview()
        {
            int blockWidth = PBoxPreview.Width / ShapePreview.Size.X / 2;
            int blockHeight = PBoxPreview.Height / ShapePreview.Size.Y / 2;
            int blockSize = Math.Min(blockWidth, blockHeight);

            GameRenderer.Render(ShapePreview, PBoxPreview, blockSize);
        }

        public void RenderGame()
        {
            int blockSize = PBoxGame.Height / GameField.Size.Y;
            GameRenderer.Render(GameField, PBoxGame, blockSize);
        }
    }
}
