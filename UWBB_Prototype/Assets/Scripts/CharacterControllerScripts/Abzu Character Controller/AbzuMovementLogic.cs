using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController.Abzu
{
    public class AbzuMovementLogic : IMovementLogic
    {
        [SerializeField] private AbzuInputLogic inputLogic;
        [SerializeField] private AbzuCameraLogic camLogic;

        [SerializeField] private CharacterControllerConfigs configs;

        private void Update()
        {
            AbzuInputState inputState = new();
            HandleCharacterMovementDirection(inputState.moveDirectionInput);
        }

        private void HandleCharacterMovementDirection(Vector2 input)
        {
            
        }
    }
}