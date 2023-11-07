using UnityEngine;

namespace UWBB.CharacterController.Abzu
{
    public class AbzuMovementController : MonoBehaviour
    {
        [SerializeField] private AbzuInputController inputController;
        [SerializeField] private AbzuCameraController camController;

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