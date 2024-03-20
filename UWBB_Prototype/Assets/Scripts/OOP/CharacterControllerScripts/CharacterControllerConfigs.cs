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
        
        [Header("CONTROLLER SETTINGS")]
        public FirstVersionControllerSettings firstVersionControllerSettings;
        public AbzuControllerSettings abzuControllerSettings;
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
    public class FirstVersionControllerSettings
    {
        [Header("Movement parameters")]
        public float speed = 10;
        public float cameraRotationSpeed = 180;
        [Header("Lock-on parameters")]
        [Tooltip(
            "Tolerance of the angle between camera and target for lock on to work. Higher value => more tolerant. \n \n" +
            "A value of 0 corresponds to a tolerance of 0°. \nA value of 1 corresponds to a tolerance of 180°.")]
        public float lockOnAngleTolerance;
        public float lockOnRange;
        [Header("Miscellaneous")]
        public float yRotationDeadZoneAngle = 89f;
        public float minAngleToYRotationDeadZone = .5f;
    }

    [Serializable]
    public class AbzuControllerSettings
    {
        [Header("Movement parameters")]
        public float speed = 10;
        [Header("Miscellaneous")]
        public float yRotationDeadZoneAngle = 89f;
        public float minAngleToYRotationDeadZone = .5f;
    }
}
