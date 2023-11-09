using UWBB.Interfaces;

namespace UWBB.CharacterController.Abzu
{
    public class AbzuInputLogic : PlayerLogic<AbzuInputState, AbzuInputState>, IInputLogic
    {
        public AbzuControls controls { get; set; }

        public void Init()
        {
            controls = new AbzuControls();
        }

        public IInputState GetInputState()
        {
            throw new System.NotImplementedException();
        }

        public override AbzuInputState RunUpdateInternal(AbzuInputState inputState)
        {
            throw new System.NotImplementedException();
        }
    }

    public struct AbzuInputState : IInputState<AbzuInputState>
    {
        
    }
}