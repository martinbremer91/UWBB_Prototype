using UnityEngine;

namespace UWBB.CharacterController
{
    [CreateAssetMenu(fileName = "CharacterControllerConfigs", menuName = "UWBB/CharacterController")]
    public class CharacterControllerConfigs : ScriptableObject
    {
        [SerializeField] private PlayerCharacterConfigs playerCharacter;
        [SerializeField] private PlayerCameraConfigs playerCamera;
        [SerializeField] private LockOnConfigs lockOn;
        
        public float speed => playerCharacter.speed;
        public float rotationSpeed => playerCharacter.rotationSpeed;
        
        public float cameraRotationSpeed => playerCamera.cameraRotationSpeed;
        public float cameraSmoothingSpeed => playerCamera.cameraSmoothingSpeed;
        public float cameraClampDotProduct => playerCamera.cameraClampDotProduct;
        
        public float lockOnAngleTolerance => lockOn.lockOnAngleTolerance;
        public float lockOnRange => lockOn.lockOnRange;
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
        public float cameraClampDotProduct;
    }

    [System.Serializable]
    public class LockOnConfigs
    {
        public float lockOnAngleTolerance;
        public float lockOnRange;
    }
}
