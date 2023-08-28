using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "CharacterControllerConfigs", menuName = "CharacterController", order = 0)]
    public class CharacterControllerConfigs : ScriptableObject
    {
        [Header("Movement parameters")]
        public float speed;
        
        [Space(10)]
        [Header("Miscellaneous")]
        public float yRotationDeadZoneAngle;
        public float minAngleToYRotationDeadZone;
    }
}
