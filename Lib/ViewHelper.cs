using UnityEngine;

namespace Lib
{
    public static class ViewHelper
    {
        private static Matrix4x4 viewMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

        public static Vector3 ToIso(this Vector3 input) => viewMatrix.MultiplyPoint3x4(input);
    }
}
