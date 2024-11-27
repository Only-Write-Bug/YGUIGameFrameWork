using System;
using System.Collections.Generic;
using OdinSerializer;
using UnityEngine;
using util;
using Object = UnityEngine.Object;

namespace GameFrame.YAssetManage.PrefabsManage
{
    [Serializable]
    public class SerializablePrefab : SerializableGameObject
    {
        public string prefabPath;
    }

    [Serializable]
    public class SerializableGameObject
    {
        public string name;
        public SerializableTransformData transform = new SerializableTransformData();
        public List<SerializableGameObject> children = new List<SerializableGameObject>();
        public List<SerializableComponent> components = new List<SerializableComponent>();
    }

    [Serializable]
    public class SerializableComponent
    {
        public string type;
        public Dictionary<string, object> fields = new Dictionary<string, object>();
        public Dictionary<string, object> properties = new Dictionary<string, object>();
    }

    [Serializable]
    public class SerializableTransformData
    {
        public float[] localPosition;
        public float[] localRotation;
        public float[] localScale;
    }

    public static class PrefabSerializer
    {
        public static string Serialize(GameObject go, string path)
        {
            var serializedPrefab = new SerializablePrefab
            {
                name = go.name,
                prefabPath = path,
                children = serializeGameObjects(go)
            };
            serializedPrefab.components = serializeComponents(go, serializedPrefab);

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
                };
                serializedGO.components = serializeComponents(child.gameObject, serializedGO);
                result.Add(serializedGO);
            }

            return result;
        }

        private static List<SerializableComponent> serializeComponents(GameObject go,
            SerializableGameObject serializableGameObject)
        {
            var result = new List<SerializableComponent>();

            foreach (var component in go.GetComponents<Component>())
            {
                if (component == null)
                    continue;

                if (component is Transform transform)
                {
                    serializableGameObject.transform.localPosition = transform.localPosition.ToArray();
                    serializableGameObject.transform.localRotation = transform.localRotation.ToArray();
                    serializableGameObject.transform.localScale = transform.localScale.ToArray();
                    continue;
                }

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

                // foreach (var property in component.GetType().GetProperties())
                // {
                //     if (!property.CanRead || property.GetIndexParameters().Length > 0 ||
                //         property.GetCustomAttributes(typeof(System.ObsoleteAttribute), true).Length > 0)
                //         continue;
                //
                //     serializedComponent.properties[property.Name] = property.GetValue(component);
                // }

                result.Add(serializedComponent);
            }

            return result;
        }

        public static GameObject Deserialize(string content)
        {
            var serializedPrefab =
                SerializationUtility.DeserializeValue<SerializablePrefab>(System.Text.Encoding.UTF8.GetBytes(content),
                    DataFormat.JSON);

            var result = new GameObject(serializedPrefab.name);
            ResetTransform(result, serializedPrefab.transform);
            DeserializeComponents(serializedPrefab.components, result);
            DeserializeGameObjects(serializedPrefab.children, result);

            Logger.Log("Generated prefab successfully, prefab name: " + serializedPrefab.name);
            return result;
        }

        private static void DeserializeGameObjects(List<SerializableGameObject> children, GameObject root)
        {
            foreach (var child in children)
            {
                var go = new GameObject(child.name);
                go.transform.SetParent(root.transform, false);
                ResetTransform(go, child.transform);

                DeserializeComponents(child.components, go);
                DeserializeGameObjects(child.children, go);
            }
        }

        private static void DeserializeComponents(List<SerializableComponent> components, GameObject go)
        {
            foreach (var componentData in components)
            {
                var type = ObjectUtil.GetTypeByName(componentData.type);
                if (type == null)
                {
                    Logger.Warn(
                        $"Deserialize Component has failed, type not found, Type:{componentData.type}, go name:{go.name}, parent:{go.transform.parent.name}");
                    continue;
                }

                var component = go.TryGetComponent(type, out var curComponent) ? curComponent : go.AddComponent(type);
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
                                $"Set Component Field has failed, cause:{e.Message}, field name:{targetField.Name}, Type:{componentData.type}, go name:{go.name}, parent:{go.transform.parent.name}");
                        }
                    }
                }

                // foreach (var targetProperty in type.GetProperties())
                // {
                //     if (!targetProperty.CanWrite || targetProperty.GetIndexParameters().Length > 0)
                //         continue;
                //
                //     if (componentData.fields.TryGetValue(targetProperty.Name, out var value))
                //     {
                //         try
                //         {
                //             targetProperty.SetValue(component, value);
                //         }
                //         catch (Exception e)
                //         {
                //             Logger.Warn(
                //                 $"Set Component Property has failed, cause:{e.Message}, property name:{targetProperty.Name}, Type:{componentData.type}, go name:{go.name}, parent:{go.transform.parent.name}");
                //         }
                //     }
                // }
            }
        }

        private static void ResetTransform(GameObject go, SerializableTransformData data)
        {
            var transform = go.GetComponent(typeof(Transform)) as Transform;
            if (transform == null)
            {
                Logger.Warn($"not found transform, GameObject name:{go.name}");
                return;
            }

            transform.localPosition = TransformUtil.ArrayToVector3(data.localPosition);
            transform.localRotation = TransformUtil.ArrayToQuaternion(data.localRotation);
            transform.localScale = TransformUtil.ArrayToVector3(data.localScale);
        }
    }
}