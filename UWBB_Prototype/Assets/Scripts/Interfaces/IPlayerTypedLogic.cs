namespace UWBB.Interfaces
{
    public interface IPlayerLogic<T> where T : IPlayerLogicData
    {
        public T RunUpdate(IInputState inputState);
    }

    public interface IPlayerTypedLogic<in T, U> : IPlayerLogic<U> 
        where T : IInputState 
        where U : IPlayerLogicData
    {
        public U RunUpdateInternal(T inputState);
    }
    
    public interface IPlayerLogicData {}
}