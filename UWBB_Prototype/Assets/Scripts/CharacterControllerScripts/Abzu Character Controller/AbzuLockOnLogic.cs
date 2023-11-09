using UWBB.Interfaces;

namespace UWBB.CharacterController.Abzu
{
    public class AbzuLockOnLogic : PlayerLogic<AbzuInputState, AbzuLockOnData>
    {
        public override AbzuLockOnData RunUpdateInternal(AbzuInputState inputState)
        {
            throw new System.NotImplementedException();
        }
    }
    
    public struct AbzuLockOnData : ILockOnLogicData {}
}