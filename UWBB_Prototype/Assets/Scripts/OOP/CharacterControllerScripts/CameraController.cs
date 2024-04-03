using UnityEngine;

namespace UWBB.CharacterController
{
    public class CameraController
    {
        private Transform camera;
        
        public void Init(Player p) => camera = p.cameraTransform;
        
        public void ProcessCameraData(PlayerCameraData cameraData)
        {
            camera.RotateAround(cameraData.pivotPoint, cameraData.rotationYAxis, cameraData.angleY);
            camera.RotateAround(cameraData.pivotPoint, cameraData.rotationXAxis, cameraData.angleX);
        }
    }
}
