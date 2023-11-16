using UWBB.Interfaces;

namespace UWBB.CharacterController.Abzu
{
    public class AbzuInputLogic : 
        IInputLogic<IInputState>,
        IInputLogic<AbzuInputState>
    {
        private readonly AbzuControls controls = new();
        private AbzuInputState inputState;

        public void Init()
        {
            controls.Enable();
            
            AbzuControls.FreeMovementActions freeMovement = controls.FreeMovement;
        }

        public void Deinit() => controls.Disable();

        IInputState IInputLogic<IInputState>.GetInputState()
            => (this as IInputLogic<AbzuInputState>).GetInputState();

        AbzuInputState IInputLogic<AbzuInputState>.GetInputState()
        {
            // DebugPanel.CustomDebug(
            //     $"Abzu\nMovement = {inputState.characterPlaneInput}\n" +
            //     $"Camera = {inputState.characterAxisInput}", DebugFlags.Input);
            return inputState;
        }
    }

    public struct AbzuInputState : IInputState
    {
        
    }
}