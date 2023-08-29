using UnityEngine;

namespace BoidsExperiment
{
    [CreateAssetMenu(menuName = "Boids/Configs", fileName = "BoidsConfigs")]
    public class BoidsConfigs : ScriptableObject
    {
        public float speed;
        public float range;
    }
}
