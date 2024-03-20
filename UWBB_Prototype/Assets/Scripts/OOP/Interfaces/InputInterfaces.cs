namespace UWBB.Interfaces
{
    public interface IInputLogic<T> : IInitializable, IDeinitializable where T : IInputState
    {
        public T GetInputState();
    }
    
    public interface IInputState : IPlayerLogicData {}
}