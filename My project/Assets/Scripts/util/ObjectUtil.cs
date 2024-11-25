using System;
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

        public static Type GetTypeByName(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null)
                return type;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(typeName);
                if (type != null)
                    return type;
            }

            return null;
        }
    }
}