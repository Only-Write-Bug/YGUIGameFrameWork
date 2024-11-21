using System.IO;
using GameFrame.YAssetManage.PrefabsManage;
using UnityEditor;
using UnityEngine;

namespace Editor.Scripts
{
    public class EditorPrefabManager
    {
        [MenuItem("Assets/YGUI/Export Prefab", false, 9)]
        public static void ExportPrefab()
        {
            var selectedObjects = Selection.objects;

            foreach (var selectedObject in selectedObjects)
            {
                var path = AssetDatabase.GetAssetPath(selectedObject);

                Debug.Log(Path.GetExtension(path).ToLower());
                if (!AssetDatabase.IsValidFolder(path) && Path.GetExtension(path).ToLower() == ".prefab")
                {
                    var content = PrefabSerializer.Serialize(selectedObject as GameObject, path);
                }
            }
        }
    }
}