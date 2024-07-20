using System.Collections.Generic;
using UnityEngine;

namespace UWBB.CharacterController
{
    public class Character_Player : MonoBehaviour, ICharacter
    {
        public MonoBehaviour monoBehaviour => this;
        public CharacterConfigs characterConfigs => playerConfigs;
        Dictionary<ControllerType, ICharacterController> ICharacter.controllers { get; set; }
        
        public Transform cameraTransform;
        [SerializeField] private Transform modelTransform;
        [SerializeField] private GameObject weaponGameObject;
        public CharacterConfigs playerConfigs;

        public readonly CharacterController_Input inputController = new();
        public readonly CharacterController_StateMachine stateMachineController = new();
        public readonly CharacterController_Movement movementController = new();
        public readonly CharacterController_Combat combatController = new();
        public readonly CharacterController_Stamina staminaController = new();
        public readonly CharacterController_Camera cameraController = new();
        public readonly CharacterController_Animation animationController = new();

        private void Awake()
        {
            (this as ICharacter).RegisterControllers();
            
            inputController.Init(this);
            stateMachineController.Init(this);
            movementController.Init(this);
            combatController.Init(this);
            staminaController.Init(this);
            cameraController.Init(this);
            animationController.Init(this);
        }
        
        void ICharacter.RegisterControllers()
        {
            (this as ICharacter).controllers = new()
            {
                { ControllerType.Input, inputController },
                { ControllerType.StateMachine, stateMachineController },
                { ControllerType.Animation, animationController },
                { ControllerType.Movement, movementController },
                { ControllerType.Stamina, staminaController },
                { ControllerType.Combat, combatController },
                { ControllerType.Camera, cameraController }
            };
        }

        private void Update()
        {
            inputController.Update();
            stateMachineController.Update();
        }
    }
}
