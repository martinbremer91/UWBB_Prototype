using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController.Abzu
{
    public class AbzuLockOnLogic : 
        IPlayerLogic<IInputState, ILockOnLogicData>,
        IPlayerLogic<AbzuInputState, AbzuLockOnData>
    {
        public ILockOnLogicData RunUpdate(IInputState inputState) 
            => RunUpdate((AbzuInputState) inputState);

        public AbzuLockOnData RunUpdate(AbzuInputState inputState)
        {
            Debug.Log("AbzuLockOn RunUpdate");
            return default;
        }
    }
    
    public struct AbzuLockOnData : ILockOnLogicData {}
}