using Unity.Entities;
using UnityEngine;

public class BoidAuthoring : MonoBehaviour
{
    public float speed = 1;
    
    public class Baker : Baker<BoidAuthoring>
    {
        public override void Bake(BoidAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<BoidSpeed>(entity, new BoidSpeed {value =  authoring.speed});
        }
    } 
}

public struct BoidSpeed : IComponentData
{
    public float value;
}
