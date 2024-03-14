using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController.Abzu
{
    public class AbzuMovementLogic : 
        IPlayerLogic<IInputState, IMovementLogicData>,
        IPlayerLogic<AbzuInputState, AbzuMovementData>
    {
        public void Init(Player player)
        {
            
        }
        
        public IMovementLogicData RunUpdate(IInputState inputState) 
            => RunUpdate((AbzuInputState)inputState);

        public AbzuMovementData RunUpdate(AbzuInputState inputState)
        {
            Debug.Log("AbzuMovement RunUpdate");
            return default;
        }
    }

    public struct AbzuMovementData : IMovementLogicData
    {
        public Vector3 movementVector { get; set; }
        public bool dashCommand { get; set; }
    }
}