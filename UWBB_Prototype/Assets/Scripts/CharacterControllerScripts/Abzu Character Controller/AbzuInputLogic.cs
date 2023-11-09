using UWBB.Interfaces;

namespace UWBB.CharacterController.Abzu
{
    public class AbzuInputLogic : IInputLogic<AbzuControls>
    {
        public AbzuControls controls { get; set; }

        public void Init()
        {
            controls = new AbzuControls();
        }
    }

    public struct AbzuInputState : IInputState<AbzuInputState>
    {
        
    }
}