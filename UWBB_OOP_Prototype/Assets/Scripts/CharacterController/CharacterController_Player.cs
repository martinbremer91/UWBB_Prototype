using UnityEngine;

namespace UWBB.CharacterController
{
    public class CharacterController_Player : MonoBehaviour
    {
        public Transform cameraTransform;
        [SerializeField] private Transform modelTransform;
        [SerializeField] private GameObject weaponGameObject;

        public GameManager gameManager;
        private CharacterController_Input inputController;
        private CharacterController_Stamina staminaController;
        private CharacterController_StateMachine stateMachine;
        private CharacterController_Camera cameraController;

        private void OnEnable()
        {
            if (GameManager.characterControllerPlayer == null)
                GameManager.characterControllerPlayer = this;
        }

        private void Start()
        {
            Init();
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        private void Init()
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
