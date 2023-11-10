using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UWBB.CharacterController
{
    [CreateAssetMenu(fileName = "CharacterControllerConfigs", menuName = "UWBB/CharacterController")]
    public class CharacterControllerConfigs : ScriptableObject
    {
        [FormerlySerializedAs("activeMovementLogic")] [Header("ACTIVE CONTROLLER SETTINGS")] 
        public MovementLogicType activeMovementType;
        [FormerlySerializedAs("activeLockOnLogic")] public LockOnLogicType activeLockOnType;
        
        [Header("CONTROLLER PRESETS")]
        public DefaultCharacterControllerData defaultControllerData;
    }

    public enum MovementLogicType
    {
        None,
        FirstVersion,
        Abzu
    }

    public enum LockOnLogicType
    {
        None,
        FirstVersion,
        Abzu
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
