using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFrame.YAssetManage.PrefabsManage
{
    [Serializable]
    public class SerializablePrefab
    {
        public string prefabName;
        public string prefabPath;
        public List<SerializableGameObject> children = new List<SerializableGameObject>();
    }

    [Serializable]
    public class SerializableGameObject
    {
        public string name;
        public List<SerializableComponent> components = new List<SerializableComponent>();
    }

    [Serializable]
    public class SerializableComponent
    {
        public string type;
        public Dictionary<string, object> fields = new Dictionary<string, object>();
    }
    
    public class PrefabSerializer
    {
        public static string Serialize(GameObject go)
        {
            var serializablePrefab = new SerializablePrefab
            {
                prefabName = go.name,
                children = serializeGameObjects(go)
            };
            
            return "";
        }

        private static List<SerializableGameObject> serializeGameObjects(GameObject go)
        {
            var result = new List<SerializableGameObject>();

            foreach (var child in go.transform)
            {
                
            }

            return result;
        }

        public static GameObject DeSerialize()
        {
            return null;
        }
    }
}