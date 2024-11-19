using System;
using System.Collections.Generic;

namespace common.CustomDataStruct
{
    [Serializable]
    public class SerializableKVPair
    {
        public string key;
        public string value;
    }

    public static class SerializableKVPairFunc
    {
        public static bool TryGetValue(this List<SerializableKVPair> self, string key, out string value)
        {
            value = "";
            foreach (var kv in self)
            {
                if (kv.key == key)
                {
                    value = kv.value;
                    return true;
                }
            }

            return false;
        }

        public static void SetValue(this List<SerializableKVPair> self, string key, string value)
        {
            foreach (var kv in self)
            {
                if (kv.key == key)
                {
                    kv.value = value;
                    return;
                }
            }
            
            self.Add(new SerializableKVPair
            {
                key = key,
                value = value,
            });
        }
    }
}