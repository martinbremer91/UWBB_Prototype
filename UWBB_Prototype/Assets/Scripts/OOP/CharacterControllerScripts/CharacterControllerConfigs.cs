using UnityEngine;

namespace UWBB.CharacterController
{
    [CreateAssetMenu(fileName = "CharacterControllerConfigs", menuName = "UWBB/CharacterController")]
    public class CharacterControllerConfigs : ScriptableObject
    {
        [SerializeField] private PlayerCharacterConfigs pcConfigs;
        [SerializeField] private PlayerCameraConfigs pcamConfigs;
        
        public float speed => pcConfigs.speed;
        public float rotationSpeed => pcConfigs.rotationSpeed;
        
        public float cameraRotationSpeed => pcamConfigs.cameraRotationSpeed;
        public float cameraSmoothingSpeed => pcamConfigs.cameraSmoothingSpeed;
        public float lockOnAngleTolerance => pcamConfigs.lockOnAngleTolerance;
        public float lockOnRange => pcamConfigs.lockOnRange;
    }

    [System.Serializable]
    public class PlayerCharacterConfigs
    {
        public float speed;
        public float rotationSpeed;
    }

    [System.Serializable]
    public class PlayerCameraConfigs
    {
        public float cameraRotationSpeed;
        public float cameraSmoothingSpeed;
        public float lockOnAngleTolerance;
        public float lockOnRange;
    }
}
