using UnityEngine;
using UnityEngine.InputSystem.Interactions;

namespace UWBB.CharacterController
{
    public class CharacterController_Input : ICharacterController
    {
        public readonly InputState inputState = new();
        private DefaultControls controls;
        private DefaultControls.GameplayActions gameplayActions;

        public void Init(ICharacter character)
        {
            controls = new();
            gameplayActions = controls.Gameplay;
            
            controls.Enable();
            gameplayActions.Enable();
            
            InitDodgeRunAction();
        }

        private void InitDodgeRunAction()
        {
            var dodgeRunAction = gameplayActions.DodgeRun;

            dodgeRunAction.performed += context =>
            {
                if (context.interaction is TapInteraction)
                    inputState.dodgeCommand = true;
                else if (context.interaction is HoldInteraction)
                {
                    inputState.dodgeCommand = false;
                    inputState.runCommand = true;
                }
            };

            dodgeRunAction.canceled += _ => { inputState.runCommand = false; };
        }

        public void Update()
        {
            inputState.cameraAim = gameplayActions.CameraAim.ReadValue<Vector2>();
            inputState.moveDirection = gameplayActions.MovementDirection.ReadValue<Vector2>();
            inputState.lightAttackCommand = gameplayActions.LightAttack.WasPressedThisFrame();
            inputState.heavyAttackCommand = gameplayActions.HeavyAttack.WasPressedThisFrame();
            inputState.heavyAttackHeld = gameplayActions.HeavyAttack.IsPressed();
            inputState.useItemCommand = gameplayActions.UseItem.WasPressedThisFrame();
            
            if (!gameplayActions.DodgeRun.WasReleasedThisFrame() && !gameplayActions.DodgeRun.IsPressed())
                inputState.dodgeCommand = false;
        }
    }

    public class InputState
    {
        public Vector2 cameraAim;
        public Vector2 moveDirection;
        public bool lightAttackCommand;
        public bool heavyAttackCommand;
        public bool heavyAttackHeld;
        public bool runCommand;
        public bool dodgeCommand;
        public bool useItemCommand;
    }
}
