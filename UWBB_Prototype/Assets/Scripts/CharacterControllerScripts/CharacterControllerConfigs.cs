using System;
using UnityEngine;

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
