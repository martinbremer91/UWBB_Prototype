namespace UWBB.Interfaces
{
    public interface IInputLogic
    {
        public void Init();
    }
    
    public interface IInputState : IPlayerLogicData {}
    public interface IInputState<T> : IInputState where T : struct {}
}