using Zmey.shared;

namespace Zmey.snake
{
    internal class SnakeGameLogic : BaseGameLogic
    {
        private SnakeGameplayState gameplayState = new SnakeGameplayState();

        public override void OnArrowUp()
        {
            gameplayState.SetDirection(SnakeDir.Up);
        }

        public override void OnArrowDown()
        {
            gameplayState.SetDirection(SnakeDir.Down);
        }

        public override void OnArrowLeft()
        {
            gameplayState.SetDirection(SnakeDir.Left);
        }

        public override void OnArrowRight()
        {
            gameplayState.SetDirection(SnakeDir.Right);
        }

        public void GotoGameplay()
        {
            gameplayState.Reset();
        }
        public override void Update(float deltaTime)
        {
            gameplayState.Update(deltaTime);
        }
    }
}