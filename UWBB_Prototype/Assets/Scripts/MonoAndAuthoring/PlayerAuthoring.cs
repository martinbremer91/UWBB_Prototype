using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public class Baker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new Player {value = entity});
            AddComponent(entity, new PlayerInput
            {
                playerPlaneInput = default,
                cameraInput = default,
            });
            AddComponent(entity, new SnapToHorizonPlane());
            SetComponentEnabled<SnapToHorizonPlane>(entity, false);
            AddComponent(entity, new LockOnToggle());
            SetComponentEnabled<LockOnToggle>(entity, false);
        }
    }
}


