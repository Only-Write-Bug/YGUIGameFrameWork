using System;
using System.Collections.Generic;
using System.Linq;

namespace util
{
    public class EnumUtil
    {
        public static Queue<Enum> GetEnumValuesSmallerThan<TEnum>(TEnum value) where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Where(v => Comparer<TEnum>.Default.Compare(v, value) < 0)
                .Cast<Enum>()
                .ToList()
                .ToQueue();
        }

        public static Queue<Enum> GetEnumValuesLargerThan<TEnum>(TEnum value) where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Where(v => Comparer<TEnum>.Default.Compare(v, value) > 0)
                .Cast<Enum>()
                .ToList()
                .ToQueue();
        }
    }
}