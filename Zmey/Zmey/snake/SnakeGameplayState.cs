using Zmey.shared;

namespace Zmey.snake
{
    internal enum SnakeDir
    {
        Up,
        Down,
        Left,
        Right
    }

    internal class SnakeGameplayState : BaseGameState
    {
        public struct Cell
        {
            public int X { get; }
            public int Y { get; }

            public Cell(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private List<Cell> _body = new();
        private SnakeDir currentDir = SnakeDir.Right;
        private float _timeToMove = 0f;
        private const char _snakeSymbol = '■';

        public int fieldWidth;
        public int fieldHeight;

        public void SetDirection(SnakeDir newDirection)
        {
            currentDir = newDirection;
        }

        public Cell ShiftTo(Cell currentCell)
        {
            return currentDir switch
            {
                SnakeDir.Up => new Cell(currentCell.X, currentCell.Y - 1),
                SnakeDir.Down => new Cell(currentCell.X, currentCell.Y + 1),
                SnakeDir.Left => new Cell(currentCell.X - 1, currentCell.Y),
                SnakeDir.Right => new Cell(currentCell.X + 1, currentCell.Y),
                _ => currentCell
            };
        }

        public override void Reset()
        {
            _body.Clear();
            var middleX = (int) (fieldWidth * 0.5);
            var middleY = (int) (fieldHeight * 0.5);
            currentDir = SnakeDir.Right;
            _body.Add(new (middleX, middleY));
            _timeToMove = 0f;
        }

        public override void Update(float deltaTime)
        {
            if (_body.Count == 0)
                return;
            _timeToMove -= deltaTime;
            if (_timeToMove > 0f)
                return;
            _timeToMove = 1f / 5f;

            var head = _body[0];
            var nextCell = ShiftTo(head);

            if (_body.Count > 0)
            {
                _body.RemoveAt(_body.Count - 1);
            }

            _body.Insert(0, nextCell);
        }

        public override void Draw(ConsoleRenderer renderer)
        {
            for (int i = 0; i < _body.Count; i++)
            {
                var segment = _body[i];

                if (i == 0)
                {
                    renderer.SetPixel(segment.X, segment.Y, _snakeSymbol, 1);
                }
                else
                {
                    renderer.SetPixel(segment.X, segment.Y, _snakeSymbol, 2);
                }
            }
        }
    }
}