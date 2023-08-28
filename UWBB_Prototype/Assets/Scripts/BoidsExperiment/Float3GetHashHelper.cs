using Unity.Mathematics;

namespace BoidsExperiment
{
    public static class Float3GetHashHelper
    {
        /// <summary>
        /// Generates unique hash code for a given float3, no matter if its elements are positive or negative.
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static int GetUniqueHashCode(this float3 f)
        {
            string unique = f.ToString();
            return unique.GetHashCode();
        }
    }
}