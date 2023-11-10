using System;

namespace UWBB.Interfaces
{
    public interface IPlayerLogic<in T, U> 
        where T : IInputState 
        where U : IPlayerLogicData
    {
        public U RunUpdate(T inputState);
    }

    public interface IPlayerLogicData {}
}