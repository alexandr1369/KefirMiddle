using UnityEngine;

namespace Utils
{
    public static class TransformExtensions
    {
        public static void SetX(this Transform transform, float x)
        {
            var pos = transform.position;
            pos.x = x;
            transform.position = pos;
        }

        public static void SetY(this Transform transform, float y)
        {
            var pos = transform.position;
            pos.y = y;
            transform.position = pos;
        }

        public static void SetZ(this Transform transform, float z)
        {
            var pos = transform.position;
            pos.z = z;
            transform.position = pos;
        }

        public static Vector3 SetVector3(this float ratio) => new Vector3(ratio, ratio, ratio);
    }
}