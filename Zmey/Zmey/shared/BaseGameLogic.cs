namespace Zmey.shared
{
    internal abstract class BaseGameLogic : ConsoleInput.IArrowListener
    {
        public void InitializeInput(ConsoleInput input)
        {
            input.Subscribe(this);
        }

        public abstract void Update(float deltaTime);

        public virtual void OnArrowDown()
        {
            throw new NotImplementedException();
        }

        public virtual void OnArrowLeft()
        {
            throw new NotImplementedException();
        }

        public virtual void OnArrowRight()
        {
            throw new NotImplementedException();
        }

        public virtual void OnArrowUp()
        {
            throw new NotImplementedException();
        }
    }
}