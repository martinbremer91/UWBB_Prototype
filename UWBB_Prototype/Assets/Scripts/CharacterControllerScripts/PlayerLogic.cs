using System;
using UWBB.Interfaces;

namespace UWBB.CharacterController
{
    public abstract class PlayerLogic<T> : IPlayerLogic<T> where T : IInputState
    {
        public void RunUpdate(IInputState inputState)
        {
            if (inputState is not T typedInputState)
                throw new Exception($"PlayerLogic {this}: input state type is incompatible with " +
                                    $"the active player logic type {typeof(T)}");
            
            RunUpdateInternal(typedInputState);
        }

        public abstract void RunUpdateInternal(T inputState);
    }
}