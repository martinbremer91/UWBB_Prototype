using UnityEngine;

namespace UWBB.CharacterController
{
    public class Character_Player : MonoBehaviour
    {
        public Transform cameraTransform;
        [SerializeField] private Transform modelTransform;
        [SerializeField] private GameObject weaponGameObject;
        public CharacterConfigs playerConfigs;

        public CharacterController_Input inputController;
        public CharacterController_StateMachine stateMachineController;
        public CharacterStatePhaseController characterStatePhaseController;
        public CharacterController_Stamina staminaController;
        public CharacterController_Camera cameraController;
        public CharacterController_Animation animationController;

        private void Awake()
        {
            inputController = new CharacterController_Input();
            stateMachineController = new CharacterController_StateMachine();
            characterStatePhaseController = new CharacterStatePhaseController();
            staminaController = new CharacterController_Stamina();
            cameraController = new CharacterController_Camera();
            animationController = new CharacterController_Animation(); 
            
            staminaController.Init(stateMachineController);
            stateMachineController.Init(this);
            cameraController.Init(cameraTransform, transform);
            animationController.Init(this);
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            inputController.Update();
            stateMachineController.Update();
        }

        private void LateUpdate()
        { 
            // characterControllerCamera.Update();
        }
    }
}
