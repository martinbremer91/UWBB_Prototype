using Unity.Entities;

namespace UWBB.Components
{
    public struct InvulnerableComponent : IComponentData
    {
        public bool invulnerable;
        public float duration;
        public float timer;
    }
}