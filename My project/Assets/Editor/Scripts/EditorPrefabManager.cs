using System.IO;
using GameFrame.YAssetManage.PrefabsManage;
using UnityEditor;
using UnityEngine;
using util;

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

                if (!AssetDatabase.IsValidFolder(path) && Path.GetExtension(path).ToLower() == ".prefab")
                {
                    var content = PrefabSerializer.Serialize(selectedObject as GameObject, path);
                    AssetUtil.GenerateAssetKey(path);
                }
                else
                {
                    Debug.LogWarning("Export prefab failed, selected object is not prefab, path:" + path);
                }
            }
        }
    }
}