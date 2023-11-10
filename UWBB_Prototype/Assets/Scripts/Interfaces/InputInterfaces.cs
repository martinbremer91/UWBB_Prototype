namespace UWBB.Interfaces
{
    public interface IInputLogic<T> where T : IInputState
    {
        
        
        public void Init();
        public void Deinit();
        
        public T GetInputState();
    }
    
    public interface IInputState : IPlayerLogicData {}
}