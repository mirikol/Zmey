namespace Zmey.shared
{
    internal abstract class BaseGameLogic : ConsoleInput.IArrowListener
    {
        protected BaseGameState? CurrentState { get; private set; }
        protected float Time {  get; private set; }
        protected int ScreenWidth { get; private set; }
        protected int ScreenHeight { get; private set; }

        public abstract void Update(float deltaTime);
        public abstract void OnArrowDown();
        public abstract void OnArrowLeft();
        public abstract void OnArrowRight();
        public abstract void OnArrowUp();
        public abstract ConsoleColor[] CreatePalette();

        public void DrawNewState(float deltaTime, ConsoleRenderer renderer)
        {
            Time += deltaTime;
            ScreenWidth = renderer.Width;
            ScreenHeight = renderer.Height;

            CurrentState?.Update(deltaTime);
            CurrentState?.Draw(renderer);

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

            CurrentState?.Reset();
            CurrentState = newState;
        }
    }
}