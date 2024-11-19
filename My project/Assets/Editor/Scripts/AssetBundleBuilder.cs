using System.Collections.Generic;
using common;
using common.CustomDataStruct;
using Editor.Scripts;
using UnityEditor;
using UnityEngine;
using util;

public class AssetBundleBuilder
{
    private static string assetBundleFolderPath = null;
    
    [MenuItem("Assets/YGUI/Build AssetBundle", false, 10)]
    public static void Build()
    {
        var selectedObjects = Selection.objects;
        var directoriesPath = filterFolderPath(selectedObjects);

        if (directoriesPath == null || directoriesPath.Length <= 0)
            return;

        GeneraAssetBundle(directoriesPath);
    }

    /// <summary>
    /// 筛选出选择对象中为文件夹的路径
    /// </summary>
    /// <param name="objs"></param>
    /// <returns></returns>
    private static string[] filterFolderPath(Object[] objs)
    {
        var result = new List<string>();
        foreach (var obj in objs)
        {
            var curObjPath = AssetDatabase.GetAssetPath(obj);
            if (AssetDatabase.IsValidFolder(curObjPath))
            {
                Debug.Log($"YGUI : AB Builder : selected folder : {curObjPath}");
                result.Add(curObjPath);
            }
        }

        return result.ToArray();
    }

    public static string GetAssetBundleFolderPath()
    {
        var settingData = LocalSettingsUtil.LoadSettings<YGUISettingData>(ESettingsFilePath.YGUI);
        if (settingData != null)
        {
            if (settingData.labels.TryGetValue(GlobalConstants.YGUI_ASSET_BUNDLE_PATH, out var path))
            {
                assetBundleFolderPath = path;
            }
            else
            {
                Debug.LogError("The storage path of the AssetBundle is not set, please check!");
            }
        }
        else
        {
            Debug.LogError("YGUI's locally stored data was not found");
        }
        
        return assetBundleFolderPath;
    }

    public static void GeneraAssetBundle(string[] folders)
    {
        GetAssetBundleFolderPath();
        if (string.IsNullOrEmpty(assetBundleFolderPath))
        {
            Debug.LogError("AssetBundle error generated");
            return;
        }

        if (folders.Length <= 0)
        {
            Debug.LogWarning("No asset folder selected");
            return;
        }
    }
}