using Unity.Entities;
using UnityEngine;
using UWBB.Components;
using UWBB.GameFramework;

namespace ECS
{
    public class PlayerAuthoring : MonoBehaviour
    {
        private class PlayerBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                if (!Application.isPlaying)
                    return;
                
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new FactionComponent()
                {
                    faction = Faction.Player,
                });
                
                AddComponent(entity, new InvulnerableComponent()
                {
                    invulnerable = false,
                    duration = Main.instance.configs.playerCombatStats.invulnerabilityDuration,
                    timer = 0
                });
            }
        }
    }
}