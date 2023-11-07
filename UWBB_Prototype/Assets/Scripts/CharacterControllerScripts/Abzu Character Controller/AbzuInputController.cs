using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController.Abzu
{
    public class AbzuInputController : IInputController<AbzuControls>
    {
        public AbzuControls controls { get; set; }

        public void Init()
        {
            controls = new AbzuControls();
        }
    }

    public struct AbzuInputState
    {
        public Vector2 moveDirectionInput;
        public Vector2 cameraAngleInput;
    }
}