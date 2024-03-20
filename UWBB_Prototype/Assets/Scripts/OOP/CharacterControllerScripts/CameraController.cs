using UnityEngine;
using UWBB.Interfaces;

namespace UWBB.CharacterController
{
    public class CameraController : IInitializable<Player>
    {
        private Transform camera;
        
        public void Init(Player p) => camera = p.cameraTransform;
        
        public void ProcessCameraData(ICameraLogicData cameraData)
        {
            camera.RotateAround(cameraData.pivotPoint, cameraData.rotationYAxis, cameraData.angleY);
            camera.RotateAround(cameraData.pivotPoint, cameraData.rotationXAxis, cameraData.angleX);
        }
    }
}
