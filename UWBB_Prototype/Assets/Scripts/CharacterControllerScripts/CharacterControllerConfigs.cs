using System;
using UnityEngine;
using UWBB.CharacterController.Abzu;
using UWBB.CharacterController.FirstVersion;

namespace UWBB.CharacterController
{
    [CreateAssetMenu(fileName = "CharacterControllerConfigs", menuName = "UWBB/CharacterController")]
    public class CharacterControllerConfigs : ScriptableObject
    {
        [Header("ACTIVE CONTROLLER SETTINGS")] 
        public MovementControllerType activeMovementController;
        public LockOnControllerType activeLockOnController;
        
        [Header("CONTROLLER PRESETS")]
        public DefaultCharacterControllerData defaultControllerData;

        [NonSerialized] public FirstVersionInputController firstVersionInputController;
        [NonSerialized] public AbzuInputController abzuInputController;
        
        private void Awake()
        {
            firstVersionInputController = new();
            firstVersionInputController.Init();

            abzuInputController = new();
            abzuInputController.Init();
        }

        public enum MovementControllerType
        {
            Default,
            Abzu
        }

        public enum LockOnControllerType
        {
            Default,
        }
    }

    [Serializable]
    public class DefaultCharacterControllerData
    {
        [Header("Movement parameters")]
        public float speed = 10;
        [Header("Miscellaneous")]
        public float yRotationDeadZoneAngle = 89f;
        public float minAngleToYRotationDeadZone = .5f;
    }
}
