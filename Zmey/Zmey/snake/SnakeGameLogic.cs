using Zmey.shared;

namespace Zmey.snake
{
    internal class SnakeGameLogic : BaseGameLogic
    {
        private SnakeGameplayState gameplayState = new SnakeGameplayState();

        public override void OnArrowUp()
        {
            if (CurrentState != gameplayState)
                return;

            gameplayState.SetDirection(SnakeDir.Up);
        }

        public override void OnArrowDown()
        {
            if (CurrentState != gameplayState)
                return;

            gameplayState.SetDirection(SnakeDir.Down);
        }

        public override void OnArrowLeft()
        {
            if (CurrentState != gameplayState)
                return;

            gameplayState.SetDirection(SnakeDir.Left);
        }

        public override void OnArrowRight()
        {
            if (CurrentState != gameplayState)
                return;

            gameplayState.SetDirection(SnakeDir.Right);
        }

        public void GotoGameplay()
        {
            gameplayState.fieldWidth = ScreenWidth;
            gameplayState.fieldHeight = ScreenHeight;
            ChangeState(gameplayState);
            gameplayState.Reset();
        }
        public override void Update(float deltaTime)
        {
            if (CurrentState != gameplayState)
                GotoGameplay();
        }

        public override ConsoleColor[] CreatePalette()
        {
            return new ConsoleColor[]
            {
                ConsoleColor.Green,
                ConsoleColor.Red,
                ConsoleColor.Black,
                ConsoleColor.Gray,
                ConsoleColor.White,
                ConsoleColor.Yellow,
                ConsoleColor.Blue,
                ConsoleColor.DarkRed
            };
        }
    }
}