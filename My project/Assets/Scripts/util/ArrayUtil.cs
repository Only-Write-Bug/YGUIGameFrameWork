using System;
using System.Collections.Generic;

namespace util
{
    public static class ArrayUtil
    {
        public static void Swap<T>(IList<T> ary, int index1, int index2)
        {
            (ary[index2], ary[index1]) = (ary[index1], ary[index2]);
        }
    }
}