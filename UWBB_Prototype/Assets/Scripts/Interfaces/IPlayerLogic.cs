namespace UWBB.Interfaces
{
    public interface IPlayerLogic
    {
        public void RunUpdate(IInputState inputState);
    }

    public interface IPlayerLogic<in T> : IPlayerLogic where T : IInputState
    {
        public void RunUpdateInternal(T inputState);
    }
}