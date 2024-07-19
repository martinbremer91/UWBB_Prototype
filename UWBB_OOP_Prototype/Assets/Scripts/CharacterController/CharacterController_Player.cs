using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Player : MonoBehaviour
    {
        public Transform cameraTransform;
        [SerializeField] private Transform modelTransform;
        [SerializeField] private GameObject weaponGameObject;
        public CharacterConfigs playerConfigs;
        public CharacterStatePhaseController characterStatePhaseController;

        private CharacterController_Input inputController;
        private CharacterController_Stamina staminaController;
        private CharacterController_StateMachine stateMachine;
        private CharacterController_Camera cameraController;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        public void Init(GameManager gameManager)
        {
            inputController = gameManager.inputController;
            staminaController = gameManager.staminaController;
            stateMachine = gameManager.stateMachine;
            cameraController = gameManager.cameraController;
        }

        private void Update()
        {
            inputController.Update();
            stateMachine.Update();
        }

        private void LateUpdate()
        { 
            // characterControllerCamera.Update();
        }
    }
}
