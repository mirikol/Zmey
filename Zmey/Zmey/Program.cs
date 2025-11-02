using Zmey.shared;
using Zmey.snake;
using System.Threading;

namespace Zmey
{
    internal class Program
    {
        private const float _targetFrameTime = 1f / 60f;

        static void Main()
        {
            var gameLogic = new SnakeGameLogic();
            var pallete = gameLogic.CreatePalette();
            var renderer0 = new ConsoleRenderer(pallete);
            var renderer1 = new ConsoleRenderer(pallete);
            
            var input = new ConsoleInput();
            gameLogic.InitializeInput(input);

            var prevRenderer = renderer0;
            var currRenderer = renderer1;

            var lastFrameTime = DateTime.Now;

            while (true)
            {
                input.Update();
                var frameStartTime = DateTime.Now;
                float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;               
                lastFrameTime = frameStartTime;

                gameLogic.DrawNewState(deltaTime, currRenderer);

                if (currRenderer != prevRenderer)
                    currRenderer.Render();

                var tmp = prevRenderer;
                prevRenderer = currRenderer;
                currRenderer = tmp;
                currRenderer.Clear();

                var nextFrameTime = frameStartTime + TimeSpan.FromSeconds(_targetFrameTime);
                var endFrameTime = DateTime.Now;

                if (nextFrameTime > endFrameTime)
                {
                    Thread.Sleep((int)(nextFrameTime - endFrameTime).TotalMilliseconds);
                }
            }
        }
    }
}