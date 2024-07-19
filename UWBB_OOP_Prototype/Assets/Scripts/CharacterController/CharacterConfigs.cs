using UnityEngine;

namespace UWBB.CharacterController
{
    [CreateAssetMenu(fileName = "CharacterConfigs_", menuName = "UWBB/CharacterConfigs")]
    public class CharacterConfigs : ScriptableObject
    {
        public float minimumRunTimerForRunningAttack = 1f;
    }
}