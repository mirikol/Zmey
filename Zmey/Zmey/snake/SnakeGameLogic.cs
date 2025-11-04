using Zmey.shared;

namespace Zmey.snake
{
    internal class SnakeGameLogic : BaseGameLogic
    {
        private bool _newGamePending = false;
        private int _currentLevel;

        private ShowTextState _showTextState = new(2);

        private SnakeGameplayState _gameplayState = new();

        public override void OnArrowUp()
        {
            if (CurrentState != _gameplayState)
                return;

            _gameplayState.SetDirection(SnakeDir.Up);
        }

        public override void OnArrowDown()
        {
            if (CurrentState != _gameplayState)
                return;

            _gameplayState.SetDirection(SnakeDir.Down);
        }

        public override void OnArrowLeft()
        {
            if (CurrentState != _gameplayState)
                return;

            _gameplayState.SetDirection(SnakeDir.Left);
        }

        public override void OnArrowRight()
        {
            if (CurrentState != _gameplayState)
                return;

            _gameplayState.SetDirection(SnakeDir.Right);
        }
        public void GoToGameOver()
        {
            _currentLevel = 0;
            _newGamePending = true;
            _showTextState.Text = "Вы проиграли.";
            ChangeState(_showTextState);
        }

        public void GotoGameplay()
        {
            _gameplayState.level = _currentLevel;
            _gameplayState.fieldWidth = ScreenWidth;
            _gameplayState.fieldHeight = ScreenHeight;
            ChangeState(_gameplayState);
            _gameplayState.Reset();
        }

        public void GoToNextLevel()
        {
            _currentLevel++;
            _newGamePending = false;
            _showTextState.Text = $"Следующий уровень: {_currentLevel}";
            ChangeState(_showTextState);
        }

        public override void Update(float deltaTime)
        {
            if (CurrentState != null && !CurrentState.IsDone())
                return;

            if (CurrentState == null || (CurrentState == _gameplayState && _gameplayState.gameOver == false))
                GoToNextLevel();

            else if (CurrentState == _gameplayState && _gameplayState.gameOver == true)
                GoToGameOver();

            else if (CurrentState != _gameplayState && _newGamePending == true)
                GoToNextLevel();

            else if (CurrentState != _gameplayState && _newGamePending == false)
                GotoGameplay();
        }

        public override ConsoleColor[] CreatePalette()
        {
            return
            [
                ConsoleColor.Green,
                ConsoleColor.Red,
                ConsoleColor.Yellow,
                ConsoleColor.Black,
                ConsoleColor.Gray,
                ConsoleColor.White,
                ConsoleColor.Blue,
                ConsoleColor.DarkRed
            ];
        }
    }
}