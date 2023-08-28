using Unity.Entities;

namespace DefaultNamespace
{
    public partial class PlayerMoveSystem : SystemBase
    {
        private Entity playerEntity;

        protected override void OnCreate()
        {
            RequireForUpdate<Player>();
            RequireForUpdate<PlayerInput>();
        }

        protected override void OnStartRunning() => playerEntity = SystemAPI.GetSingletonEntity<Player>();

        protected override void OnUpdate()
        {
            RefRO<PlayerInput> playerInput = SystemAPI.GetComponentRO<PlayerInput>(playerEntity);
            
            
        }

        protected override void OnStopRunning() => playerEntity = Entity.Null;
    }
}