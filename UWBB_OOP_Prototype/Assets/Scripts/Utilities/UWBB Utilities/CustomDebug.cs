#if UNITY_EDITOR
using TMPro;
using UnityEngine;

namespace UWBB.CharacterController
{
    public class CustomDebug : MonoBehaviour
    {
        private void OnEnable()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            } else if (instance != this)
                Destroy(gameObject);
        }
        
        private static CustomDebug instance;
        
        [SerializeField] private TMP_Text text;
        
        private CharacterController_StateMachine stateMachine;
        private CharacterController_Input input;
        private CharacterController_Stamina stamina;
        private CharacterController_Camera cameraCtrl;

        [SerializeField] private bool debugStateMachine;
        [SerializeField] private bool debugInput;
        [SerializeField] private bool debugStamina;
        [SerializeField] private bool debugCamera;

        private void Start()
        {
            Character_Player player = FindObjectOfType<Character_Player>();

            if (player == null)
            {
                Destroy(gameObject);
                return;
            }
            
            Init(player);
        }

        private void Init(Character_Player player)
        {
            stateMachine = player.stateMachineController;
            input = player.inputController;
            stamina = player.staminaController;
            cameraCtrl = player.cameraController;
        }
            
        private void Update()
        {
            string stateMachineInfo = debugStateMachine ? GetStateMachineDebugInfo() + "\n" : default;
            string inputInfo = debugInput ? GetInputDebugInfo() + "\n" : default;
            string staminaInfo = debugStamina ? GetStaminaDebugInfo() + "\n" : default;
            string cameraInfo = debugCamera ? GetCameraDebugInfo()+ "\n" : default;

            text.text = stateMachineInfo + inputInfo + staminaInfo + cameraInfo;
        }

        private string GetStateMachineDebugInfo() 
            => $"State: {stateMachine.characterState} // SubState: {stateMachine.characterSubState}\n";

        private float lAtkTimer, hAtkDownTimer, dodgeTimer;
        private string GetInputDebugInfo()
        {
            InputState inputState = input.inputState;

            if (inputState.lightAttackCommand)
                lAtkTimer = .1f;
            else
                lAtkTimer = lAtkTimer < 0 ? 0 : lAtkTimer - Time.deltaTime;
            
            if (inputState.heavyAttackCommand)
                hAtkDownTimer = .1f;
            else
                hAtkDownTimer = hAtkDownTimer < 0 ? 0 : hAtkDownTimer - Time.deltaTime;
            
            if (inputState.dodgeCommand)
                dodgeTimer = .1f;
            else
                dodgeTimer = dodgeTimer < 0 ? 0 : dodgeTimer - Time.deltaTime;
            
            string inputInfo = $"Move: {inputState.moveDirection} // Camera: {inputState.cameraAim} \n" +
                               $"L Attack: {lAtkTimer > 0} // H Attack: {hAtkDownTimer > 0} // " +
                               $"H Attack Charge: {inputState.heavyAttackHeld} // " +
                               $"Run: {inputState.runCommand} // Dodge: {dodgeTimer > 0}\n";
            
            return inputInfo;
        }
        
        private string GetStaminaDebugInfo()
        {
            return "stamina";
        }
        
        private string GetCameraDebugInfo()
        {
            return "camera";
        }
    }
}
#endif
