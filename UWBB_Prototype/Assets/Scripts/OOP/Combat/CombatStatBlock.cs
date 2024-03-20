using UnityEngine;

namespace UWBB.Combat
{
    [CreateAssetMenu(fileName = "CombatStatBlock", menuName = "UWBB/CombatStatBlock")]
    public class CombatStatBlock : ScriptableObject
    {
        public float invulnerabilityDuration;
    }
}