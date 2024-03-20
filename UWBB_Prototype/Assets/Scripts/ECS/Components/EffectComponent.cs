using Unity.Entities;

namespace UWBB.Components
{
    public struct EffectComponent : IComponentData
    {
        public EffectType type;
    }

    public enum EffectType
    {
        Attack = 0,
    }
}