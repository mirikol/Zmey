namespace Zmey
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
            currentDir = SnakeDir.Up;
            _body.Add(new (0, 0));
            _timeToMove = 0f;
        }

        public override void Update(float deltaTime)
        {
            _timeToMove -= deltaTime;
            if (_timeToMove > 0f)
                return;
            _timeToMove = 1f / 5f;

            var head = _body[0];
            var nextCell = ShiftTo(head);
            _body.RemoveAt(_body.Count - 1);

            _body.Insert(0, nextCell);
            Console.WriteLine($"{_body[0].X}, {_body[0].Y}");
        }
    }
}