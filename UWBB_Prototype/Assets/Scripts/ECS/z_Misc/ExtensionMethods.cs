using Unity.Mathematics;

namespace ECS
{
    public static class ExtensionMethods
    {
        public static float Angle(this quaternion a, quaternion b)
        {
            float dot = (float)((double)a.value.x * (double)b.value.x + (double)a.value.y * (double)b.value.y +
                                (double)a.value.z * (double)b.value.z + (double)a.value.w * (double)b.value.w);
            
            float num = math.min(math.abs(dot), 1f);
            return num > 0.9999989867210388 ? 0.0f : (float) (math.acos(num) * 2.0 * 57.295780181884766);
        }
    }
}