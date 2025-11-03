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

        private List<Cell> _body = [];
        private SnakeDir currentDir = SnakeDir.Right;
        private float _timeToMove = 0f;
        private const char _snakeSymbol = '■';
        private const char _circleSymbol = '?';
        private Cell _apple;
        private Random _random = new Random();

        public int fieldWidth;
        public int fieldHeight;

        public bool gameOver;
        public bool hasWon;
        public int level;

        private void GenerateApple()
        {
            Cell cell = new Cell(_random.Next(fieldWidth), _random.Next(fieldHeight));
            
            if (_body.Count > 0 && cell.Equals(_body[0]))
            {
                var middleY = fieldHeight / 2;

                if (cell.Y > middleY)
                {
                    cell = new Cell(cell.X, cell.Y - 1);
                }
                else
                {
                    cell = new Cell(cell.X, cell.Y + 1);
                }
            }

            _apple = cell;
        }

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
            hasWon = false;
            gameOver = false;

            _body.Clear();

            var middleX = (int) (fieldWidth * 0.5);
            var middleY = (int) (fieldHeight * 0.5);

            currentDir = SnakeDir.Right;
            _body.Add(new (middleX, middleY));
            _timeToMove = 0f;

            _apple = new Cell(middleX +5, middleY +5);
        }

        public override void Update(float deltaTime)
        {
            if (_body.Count == 0)
                return;

            _timeToMove -= deltaTime;
            if (_timeToMove > 0f || gameOver == true)
                return;

            _timeToMove = 1f / (level + 5f);

            var head = _body[0];
            var nextCell = ShiftTo(head);

            if (nextCell.X >= fieldWidth || nextCell.Y >= fieldHeight || nextCell.X < 0 || nextCell.Y < 0)
            {
                gameOver = true;
                return;
            }

            if (nextCell.X == _apple.X && nextCell.Y == _apple.Y)
            {
                _body.Insert(0, _apple);

                if (_body.Count >= level + 3)
                {
                    hasWon = true;
                }
                else
                {
                    GenerateApple();
                }
                return;
            }

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

            renderer.SetPixel(_apple.X, _apple.Y, _circleSymbol, 0);

            int applesCollected = _body.Count - 1;
            string scoreText = $"Яблочки: {applesCollected}";
            renderer.DrawString(scoreText, 1, 1, ConsoleColor.White);
                        
            string levelText = $"Уровень: {level}";
            renderer.DrawString(levelText, 1, 2, ConsoleColor.White);
        }

        public override bool IsDone()
        {            
            return hasWon || gameOver;
        }
    }
}