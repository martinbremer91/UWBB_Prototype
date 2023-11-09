using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController.FirstVersion
{
    public class FirstVersionInputLogic : PlayerLogic<FirstVersionInputState, FirstVersionInputState>
    {
        public FirstVersionControls controls { get; set; }

        public override FirstVersionInputState RunUpdateInternal(FirstVersionInputState inputState)
        {
            throw new System.NotImplementedException();
        }
    }

    public struct FirstVersionInputState : IInputState<FirstVersionInputState>
    {
        public Vector2 characterPlaneInput;
        public Vector2 characterAxisInput;

        public bool snapCommand;
        public bool lockOnToggleCommand;
    }
}
