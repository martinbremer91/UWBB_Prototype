using Unity.Entities;
using Unity.Mathematics;

public struct Player : IComponentData
{
    public Entity value;
}

public struct PlayerInput : IComponentData
{
    public float2 playerPlaneInput;
    public float2 cameraInput;
}

public struct SnapToHorizonPlane : IComponentData, IEnableableComponent {}
public struct LockOnToggle : IComponentData, IEnableableComponent {}