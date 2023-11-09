using UWBB.Interfaces;

namespace UWBB.CharacterController.Abzu
{
    public class AbzuCameraLogic : PlayerLogic<AbzuInputState, AbzuCameraData>
    {
        public override AbzuCameraData RunUpdateInternal(AbzuInputState inputState)
        {
            throw new System.NotImplementedException();
        }
    }
    
    public struct AbzuCameraData : ICameraLogicData {}
}