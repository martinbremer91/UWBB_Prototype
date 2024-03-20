using UnityEngine;
using UWBB.CharacterController;
using UWBB.Combat;

namespace UWBB.GameFramework
{
    [CreateAssetMenu(fileName = "MainConfigs", menuName = "UWBB/MainConfigs")]
    public class MainConfigs : ScriptableObject
    {
        public CharacterControllerConfigs ccConfigs;
        public CombatStatBlock playerCombatStats;
    }
}