using UnityEngine;

namespace UWBB.CharacterController
{
    [CreateAssetMenu(fileName = "CharacterControllerConfigs", menuName = "UWBB/CharacterController")]
    public class CharacterControllerConfigs : ScriptableObject
    {
        public float speed;
        public float cameraRotationSpeed;
        public float lockOnAngleTolerance;
        public float lockOnRange;
    }
}
