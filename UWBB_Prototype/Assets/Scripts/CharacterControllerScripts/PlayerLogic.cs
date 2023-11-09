using System;
using UWBB.Interfaces;

namespace UWBB.CharacterController
{
    public abstract class PlayerLogic<T, U> : IPlayerTypedLogic<T, U> 
        where T : IInputState
        where U : IPlayerLogicData
    {
        public U RunUpdate(IInputState inputState)
        {
            if (inputState is not T typedInputState)
                throw new Exception($"PlayerLogic {this}: input state type is incompatible with " +
                                    $"the active player logic type {typeof(T)}");
            
            return RunUpdateInternal(typedInputState);
        }

        public abstract U RunUpdateInternal(T inputState);
    }
}