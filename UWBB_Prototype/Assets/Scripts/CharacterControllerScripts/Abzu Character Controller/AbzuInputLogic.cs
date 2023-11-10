using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController.Abzu
{
    public class AbzuInputLogic : 
        IInputLogic<IInputState>,
        IInputLogic<AbzuInputState>
    {
        private readonly AbzuControls controls = new();

        public void Init() => controls.Enable();
        public void Deinit() => controls.Disable();

        IInputState IInputLogic<IInputState>.GetInputState()
            => (this as IInputLogic<AbzuInputState>).GetInputState();

        AbzuInputState IInputLogic<AbzuInputState>.GetInputState()
        {
            Debug.Log("AbzuInput GetInputState");
            return default;
        }
    }

    public struct AbzuInputState : IInputState
    {
        
    }
}