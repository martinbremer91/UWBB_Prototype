using UnityEngine;
using UWBB.CharacterController;

namespace UWBB.GameFramework
{
    [CreateAssetMenu(fileName = "MainConfigs", menuName = "UWBB/MainConfigs")]
    public class MainConfigs : ScriptableObject
    {
        public CharacterControllerConfigs ccConfigs;
    }
}