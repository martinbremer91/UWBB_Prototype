using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace DefaultNamespace
{
    public partial class CameraMoveSystem : SystemBase
    {
        private Entity player;

        protected override void OnCreate() => RequireForUpdate<Player>();

        protected override void OnStartRunning() => player = SystemAPI.GetSingletonEntity<Player>();

        protected override void OnUpdate()
        {
            var cameraTransform = PlayerCameraSingleton.instance.transform;
            var playerTranform = SystemAPI.GetComponent<LocalToWorld>(player);

            var position = (Vector3)playerTranform.Position;
            position -= 10f * (Vector3)playerTranform.Forward;
            position += Vector3.up * 5;
            
            cameraTransform.position = position;
            cameraTransform.LookAt(playerTranform.Position);
        }
    }
}