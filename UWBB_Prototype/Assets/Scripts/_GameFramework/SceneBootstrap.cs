using UnityEngine;
using UWBB.CharacterController;

namespace UWBB.GameFramework
{
    public class SceneBootstrap : MonoBehaviour
    {
        public Player player;
        
        // public MovementController movementController => player.movementController;
        // public CameraController cameraController => player.cameraController;
        // public LockOnController lockOnController => player.lockOnController;

        private void Awake()
        {
            player.Init();
            Destroy(gameObject);
        }
    }
}