using System;
using System.Collections.Generic;
using System.Linq;

namespace TetraSnake
{
    internal class SnakeLogic
    {
        
        public event Action<int> ScoreChanged;
        public event Action Died;

        
        public Vector Direction;
        public bool CanDieFromTetramino;

        private readonly Random _random = new Random();
        private List<BodySnake> _segments;

        public Apple Apple;

        
        public void Initialize(Field field)
        {
            _segments = new List<BodySnake> { new BodySnake(field) };
            Apple = new Apple(field);
        }

        
        public void UpdateMovement(Field field, Tetramino tetramino = null)
        {
            if (PreventSelfCollisionLoop()) 
                return;

            field.SetPoint(_segments[0].Position, CellType.Empty);

            if (!field.IsInside(_segments[0].Position))
            {
                HandleDeath(field);
                return;
            }

            var previousHead = _segments[0].Position;
            _segments[0].Position += Direction;

            if (_segments.Count(s => s.Position == _segments[0].Position) > 1)
            {
                HandleDeath(field);
                return;
            }

            switch (field.GetPoint(_segments[0].Position))
            {
                case CellType.Apple:
                    ConsumeApple(field);
                    break;

                case CellType.Tetris:
                    if (CanDieFromTetramino)
                        HandleDeath(field);
                    else
                        ConsumeTetramino(tetramino, _segments[0]);
                    break;
            }

            MoveBody(field, previousHead);

            Render(field);
        }

        private void MoveBody(Field field, Vector previousHead)
        {
            var last = previousHead;

            for (int i = 1; i < _segments.Count; i++)
            {
                var tmp = _segments[i].Position;
                _segments[i].Position = last;
                last = tmp;
                field.SetPoint(tmp, CellType.Empty);
            }
        }



        
        private void ConsumeApple(Field field)
        {
            ScoreChanged?.Invoke(40);
            Apple = new Apple(field);
            Grow();
        }

        private void ConsumeTetramino(Tetramino tetramino, BodySnake head)
        {
            var localPos = head.Position - tetramino.Position;
            if (localPos.Y >= 0 && localPos.Y < tetramino.Size.Y &&
                localPos.X >= 0 && localPos.X < tetramino.Size.X &&
                tetramino.IsSelf(head.Position))
            {
                tetramino.SetCell(localPos, CellType.Empty);
            }
        }

        private void Grow()
        {
            _segments.Add(new BodySnake(_segments.Last().Position));
        }

        private bool PreventSelfCollisionLoop()
        {
            if (_segments.Count <= 1 || _segments[0] != _segments[1])
                return false;

            Direction = new Vector(-Direction.X, -Direction.Y);
            return true;
        }

        private void Render(Field field)
        {
            foreach (var part in _segments)
                field.SetPoint(part.Position, CellType.Snake);
        }

        private void HandleDeath(Field field)
        {
            _segments.RemoveRange(1, _segments.Count - 1);

            do
            {
                _segments[0].Position = new Vector(
                    _random.Next(field.Size.X),
                    _random.Next(field.Size.Y)
                );
            } 
            while (field.GetPoint(_segments[0].Position) == CellType.Tetris);

            Direction = new Vector(0, 0);
            Died?.Invoke();
        }
    }
}
