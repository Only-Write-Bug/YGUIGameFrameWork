using System;
using System.Collections.Generic;
using OdinSerializer;
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
        public List<SerializableGameObject> children = new List<SerializableGameObject>();
        public List<SerializableComponent> components = new List<SerializableComponent>();
    }

    [Serializable]
    public class SerializableComponent
    {
        public string type;
        public Dictionary<string, object> fields = new Dictionary<string, object>();
    }
    
    public static class PrefabSerializer
    {
        public static string Serialize(GameObject go, string path)
        {
            var serializedPrefab = new SerializablePrefab
            {
                prefabName = go.name,
                prefabPath = path,
                children = serializeGameObjects(go)
            };
            
            return System.Text.Encoding.UTF8.GetString(SerializationUtility.SerializeValue(serializedPrefab, DataFormat.JSON));
        }

        private static List<SerializableGameObject> serializeGameObjects(GameObject go)
        {
            var result = new List<SerializableGameObject>();

            foreach (Transform child in go.transform)
            {
                var serializedGO = new SerializableGameObject
                {
                    name = child.gameObject.name,
                    children = serializeGameObjects(child.gameObject),
                    components = serializeComponents(child.gameObject)
                };
                result.Add(serializedGO);
            }

            return result;
        }

        private static List<SerializableComponent> serializeComponents(GameObject go)
        {
            var result = new List<SerializableComponent>();

            foreach (var component in go.GetComponents<Component>())
            {
                if (component == null) 
                    continue;

                var serializedComponent = new SerializableComponent
                {
                    type = component.GetType().FullName
                };

                foreach (var field in component.GetType().GetFields())
                {
                    if(!field.IsPublic && field.GetCustomAttributes(typeof(SerializeField), false).Length == 0)
                        continue;

                    try
                    {
                        serializedComponent.fields[field.Name] = field.GetValue(component);
                    }
                    catch
                    {
                        
                    }
                }
                
                result.Add(serializedComponent);
            }
            
            return result;
        }

        public static GameObject DeSerialize(string content)
        {
            return null;
        }
    }
}