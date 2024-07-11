using UnityEngine;

namespace UWBB.CharacterController
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform modelTransform;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private GameObject weaponGameObject;

        private CharacterControllerState ccState = new();
        
        private InputState inputState => ccState.inputState;
        private CharacterState characterState => ccState.characterState;
        private MoveSpeedState moveSpeedState => ccState.moveSpeedState;
        private StaminaState staminaState => ccState.staminaState;
        
        private CharacterController_Input inputController;
        private CharacterController_Camera characterControllerCamera;

        private void Awake()
        {
            inputController = new CharacterController_Input();
            characterControllerCamera = new CharacterController_Camera(cameraTransform, transform);
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            inputController.Update(inputState);
        }

        private void LateUpdate()
        { 
            characterControllerCamera.Update(inputState);
        }
    }

    public class CharacterControllerState
    {
        public InputState inputState = new();
        public CharacterState characterState;
        public MoveSpeedState moveSpeedState;
        public Vector3 targetMoveDirection;
        public StaminaState staminaState;
        public AttackState attackState;
    }

    public enum CharacterState
    {
        Idle,
        Walk,
        Run,
        Dodge,
        Attack,
        UsingItem,
        // Stunned,
        // StunnedActionable (cancelable getup)
    }
}
