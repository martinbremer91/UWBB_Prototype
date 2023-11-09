using UWBB.Interfaces;

namespace UWBB.CharacterController.Abzu
{
    public class AbzuMovementLogic : PlayerLogic<AbzuInputState, AbzuMovementData>
    {
        public override AbzuMovementData RunUpdateInternal(AbzuInputState inputState)
        {
            throw new System.NotImplementedException();
        }
    }

    public struct AbzuMovementData : IMovementLogicData
    {
        
    }
}