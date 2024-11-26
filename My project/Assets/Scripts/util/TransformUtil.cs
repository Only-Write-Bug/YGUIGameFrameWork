using System.Linq;
using UnityEngine;

namespace util
{
    public static class TransformUtil
    {
        public static float[] ToArray(this Vector3 self)
        {
            return new[] { self.x, self.y, self.z };
        }

        public static Vector3 ArrayToVector3(float[] arr)
        {
            ArrayUtil.CompletionByLength(arr, 3, 0);
            return new Vector3(arr[0], arr[1], arr[2]);
        }

        public static float[] ToArray(this Quaternion self)
        {
            return new[] { self.x, self.y, self.z, self.w };
        }

        public static Quaternion ArrayToQuaternion(float[] arr)
        {
            if (ArrayUtil.CompletionByLength(arr, 4, 0))
            {
                //Quaternion.w = 1
                arr[3] = 1;
            }
            return new Quaternion(arr[0], arr[1], arr[2], arr[3]);
        }
    }
}