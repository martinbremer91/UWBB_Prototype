using UnityEngine;
using UnityEngine.InputSystem.Interactions;

namespace UWBB.CharacterController
{
    public class CharacterController_Input
    {
        public readonly InputState inputState = new();
        private readonly DefaultControls controls = new();
        private DefaultControls.GameplayActions gameplayActions;

        public CharacterController_Input()
        {
            gameplayActions = controls.Gameplay;
            
            controls.Enable();
            gameplayActions.Enable();
            
            InitDodgeRunAction();
            InitHeavyAttackAction();
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
        
        private void InitHeavyAttackAction()
        {
            var heavyAttackAction = gameplayActions.HeavyAttack;

            heavyAttackAction.performed += context =>
            {
                if (context.interaction is TapInteraction)
                    inputState.heavyAttackCommand = true;
                else if (context.interaction is HoldInteraction)
                {
                    inputState.heavyAttackCommand = false;
                    inputState.heavyAttackChargeCommand = true;
                }
            };

            heavyAttackAction.canceled += _ => inputState.heavyAttackChargeCommand = false;
        }

        public void Update()
        {
            inputState.cameraAim = gameplayActions.CameraAim.ReadValue<Vector2>();
            inputState.moveDirection = gameplayActions.MovementDirection.ReadValue<Vector2>();
            inputState.lightAttackCommand = gameplayActions.LightAttack.WasPressedThisFrame();
            inputState.useItemCommand = gameplayActions.UseItem.WasPressedThisFrame();
            
            if (!gameplayActions.DodgeRun.WasReleasedThisFrame() && !gameplayActions.DodgeRun.IsPressed())
                inputState.dodgeCommand = false;
            if (!gameplayActions.HeavyAttack.WasReleasedThisFrame() && !gameplayActions.HeavyAttack.IsPressed())
                inputState.heavyAttackCommand = false;
        }
    }

    public class InputState
    {
        public Vector2 cameraAim;
        public Vector2 moveDirection;
        public bool lightAttackCommand;
        public bool heavyAttackCommand;
        public bool heavyAttackChargeCommand;
        public bool runCommand;
        public bool dodgeCommand;
        public bool useItemCommand;
    }
}
