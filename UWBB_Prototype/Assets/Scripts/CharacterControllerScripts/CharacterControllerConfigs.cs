using System;
using UnityEngine;

namespace UWBB.CharacterController
{
    [CreateAssetMenu(fileName = "CharacterControllerConfigs", menuName = "UWBB/CharacterController")]
    public class CharacterControllerConfigs : ScriptableObject
    {
        [Header("ACTIVE CONTROLLER SETTINGS")] 
        public MovementLogicType activeMovementLogic;
        public LockOnLogicType activeLockOnLogic;
        
        [Header("CONTROLLER PRESETS")]
        public DefaultCharacterControllerData defaultControllerData;

        public enum MovementLogicType
        {
            Default,
            Abzu
        }

        public enum LockOnLogicType
        {
            Default,
            Abzu
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
