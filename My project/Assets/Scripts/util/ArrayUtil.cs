using System;
using System.Collections.Generic;
using System.Linq;

namespace util
{
    public static class ArrayUtil
    {
        public static void Swap<T>(IList<T> ary, int index1, int index2)
        {
            (ary[index2], ary[index1]) = (ary[index1], ary[index2]);
        }

        /// <summary>
        /// 根据长度补全（支持数组和列表）
        /// </summary>
        /// <param name="ary">要补全的数组或列表</param>
        /// <param name="length">目标长度</param>
        /// <param name="defaultValue">不足时的默认值</param>
        /// <returns>是否进行了补全操作</returns>
        public static bool CompletionByLength<T>(ref T[] ary, int length, T defaultValue)
        {
            if (ary.Length >= length)
                return false;

            var newArray = new T[length];
            Array.Copy(ary, newArray, ary.Length);
            for (var i = ary.Length; i < length; i++)
            {
                newArray[i] = defaultValue;
            }

            ary = newArray;
            return true;
        }

        /// <summary>
        /// 根据长度补全（支持列表）
        /// </summary>
        /// <param name="ary">要补全的列表</param>
        /// <param name="length">目标长度</param>
        /// <param name="defaultValue">不足时的默认值</param>
        /// <returns>是否进行了补全操作</returns>
        public static bool CompletionByLength<T>(IList<T> ary, int length, T defaultValue)
        {
            if (ary.Count >= length)
                return false;

            for (var i = ary.Count; i < length; i++)
            {
                ary.Add(defaultValue);
            }

            return true;
        }

        public static Queue<T> ToQueue<T>(this IList<T> ary, bool isPositiveOrder = true)
        {
            if (!isPositiveOrder)
                ary = (IList<T>)ary.Reverse();

            var result = new Queue<T>();

            foreach (var element in ary)
            {
                result.Enqueue(element);
            }
            
            return result;
        }
    }
}