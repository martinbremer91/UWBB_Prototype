using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController.FirstVersion
{
    public class FirstVersionInputController : IInputController<FirstVersionControls>
    {
        public FirstVersionInputState FirstVersionInputState = new();
        public FirstVersionControls controls { get; set; }
        
        public void Init()
        {
            
        }
    }

    public struct FirstVersionInputState
    {
        public Vector2 characterPlaneInput;
        public Vector2 characterAxisInput;

        public bool snapCommand;
        public bool lockOnToggleCommand;
    }
}
