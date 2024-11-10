using UnityEngine;

namespace util
{
    public static class ObjectUtil
    {
        public static bool HasProperty(object obj, string propertyName)
        {
            foreach (var propertyInfo in obj.GetType().GetProperties())
            {
                if (propertyInfo.Name == propertyName)
                {
                    return true;
                }
            }
            return false;
        }

        public static dynamic GetPropertyValue(object obj, string propertyName)
        {
            if (!HasProperty(obj, propertyName))
            {
                return null;
            }

            return obj.GetType().GetProperty(propertyName)!.GetValue(obj);
        }
    }
}