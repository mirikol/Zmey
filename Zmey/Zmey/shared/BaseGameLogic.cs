namespace Zmey.shared
{
    internal abstract class BaseGameLogic : ConsoleInput.IArrowListener
    {
        protected BaseGameState? currentState { get; private set; }
        protected float time {  get; private set; }
        protected int screenWidth { get; private set; }
        protected int screenHeight { get; private set; }

        public BaseGameState? CurrentState => currentState;
        public float Time => time;
        public int ScreenWidth => screenWidth;
        public int ScreenHeight => screenHeight;

        public abstract void Update(float deltaTime);
        public abstract void OnArrowDown();
        public abstract void OnArrowLeft();
        public abstract void OnArrowRight();
        public abstract void OnArrowUp();
        public abstract ConsoleColor[] CreatePalette();

        public void DrawNewState(float deltaTime, ConsoleRenderer renderer)
        {
            time += deltaTime;
            screenWidth = renderer.width;
            screenHeight = renderer.height;

            currentState?.Update(deltaTime);
            currentState?.Draw(renderer);

            Update(deltaTime);
        }

        public void InitializeInput(ConsoleInput input)
        {
            input.Subscribe(this);
        }

        public void ChangeState(BaseGameState newState)
        {
            if (newState == null)
                return;

            currentState?.Reset();
            currentState = newState;
        }
    }
}