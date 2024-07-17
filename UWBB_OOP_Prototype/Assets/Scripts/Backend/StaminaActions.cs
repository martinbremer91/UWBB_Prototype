using UnityEngine;

namespace UWBB.CharacterController
{
    [CreateAssetMenu(fileName = "StaminaActions", menuName = "UWBB/StaminaActions")]
    public class StaminaActions : ScriptableObject
    {
        public StaminaAction lightAttack;
        public StaminaAction heavyAttack;

        public StaminaAction dodge;
        public StaminaAction run;

        public StaminaAction block;
    }
    
    [System.Serializable]
    public class StaminaAction
    {
        public float cost = 1f;
        public float minToPerform = 1f;
        public bool isCostOverTime;
    }
}