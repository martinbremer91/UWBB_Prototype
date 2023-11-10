using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController.FirstVersion
{
    public class FirstVersionInputLogic : 
        IInputLogic<IInputState>, 
        IInputLogic<FirstVersionInputState>
    {
        private readonly FirstVersionControls controls = new();

        public void Init() => controls.Enable();
        public void Deinit() => controls.Disable();

        IInputState IInputLogic<IInputState>.GetInputState() 
            => (this as IInputLogic<FirstVersionInputState>).GetInputState();

        FirstVersionInputState IInputLogic<FirstVersionInputState>.GetInputState()
        {
            Debug.Log("FirstVersionInput GetInputState");
            return default;
        }
    }

    public struct FirstVersionInputState : IInputState
    {
        public Vector2 characterPlaneInput;
        public Vector2 characterAxisInput;

        public bool snapCommand;
        public bool lockOnToggleCommand;
    }
}
