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

            return System.Text.Encoding.UTF8.GetString(
                SerializationUtility.SerializeValue(serializedPrefab, DataFormat.JSON));
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
                    if (!field.IsPublic && field.GetCustomAttributes(typeof(SerializeField), false).Length == 0)
                        continue;

                    serializedComponent.fields[field.Name] = field.GetValue(component);
                }

                result.Add(serializedComponent);
            }

            return result;
        }

        public static GameObject Deserialize(string content)
        {
            var serializedPrefab =
                SerializationUtility.DeserializeValue<SerializablePrefab>(System.Text.Encoding.UTF8.GetBytes(content),
                    DataFormat.JSON);

            var result = new GameObject(serializedPrefab.prefabName);
            DeserializeGameObjects(serializedPrefab.children, result);

            return result;
        }

        private static void DeserializeGameObjects(List<SerializableGameObject> children, GameObject root)
        {
            foreach (var child in children)
            {
                var go = new GameObject(child.name);
                go.transform.SetParent(root.transform, false);

                DeserializeComponents(child.components, go);
                DeserializeGameObjects(child.children, go);
            }
        }

        private static void DeserializeComponents(List<SerializableComponent> components, GameObject go)
        {
            foreach (var componentData in components)
            {
                var type = Type.GetType(componentData.type);
                if (type == null)
                {
                    Logger.Warn(
                        $"Deserialize Component has failed, type not found, Type:{componentData.type}, go name:{go.name}, parent:{go.transform.parent.name}");
                    continue;
                }

                var component = go.AddComponent(type);
                foreach (var targetField in type.GetFields())
                {
                    if (componentData.fields.TryGetValue(targetField.Name, out var value))
                    {
                        try
                        {
                            targetField.SetValue(component, value);
                        }
                        catch (Exception e)
                        {
                            Logger.Warn(
                                $"Set Component Field has failed, field name:{targetField.Name}, Type:{componentData.type}, go name:{go.name}, parent:{go.transform.parent.name}");
                        }
                    }
                }
            }
        }
    }
}