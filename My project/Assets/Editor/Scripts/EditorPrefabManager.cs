using System;
using System.IO;
using common;
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

            if (selectedObjects == null || selectedObjects.Length <= 0)
            {
                return;
            }
            
            var jsonSaveRootPath = Path.Combine(Directory.GetCurrentDirectory(),
                GlobalConstants.ASSET_SHARED_PREFABS_PATH);
            IOUtil.CheckPath(jsonSaveRootPath);

            foreach (var selectedObject in selectedObjects)
            {
                var path = AssetDatabase.GetAssetPath(selectedObject);

                if (!AssetDatabase.IsValidFolder(path) && Path.GetExtension(path).ToLower() == ".prefab")
                {
                    var content = PrefabSerializer.Serialize(selectedObject as GameObject, path);
                    var assetKey = AssetUtil.GenerateAssetKey(path);
                    var jsonPath = Path.Combine(jsonSaveRootPath, assetKey + ".json");
                    IOUtil.WriteContentToFile(jsonPath, content);
                    Debug.Log("Export prefab successfully, AssetKey: " + assetKey + ", JSON Path: " + jsonPath);
                }
                else
                {
                    Debug.LogWarning("Export prefab failed, selected object is not prefab, path:" + path);
                }
            }
        }
    }
}