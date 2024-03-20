using Unity.Entities;

namespace UWBB.Components
{
    public struct FactionComponent : IComponentData
    {
        public Faction faction;
    }

    public enum Faction
    {
        Neutral = 0,
        Player = 1 << 0,
        Enemy = 1 << 1,
    }
}