using UnityEngine;

namespace ExtensionMethods
{
    public static class VectorExtension
    {
        public static Vector2 xy(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.y);
        }
    }
}