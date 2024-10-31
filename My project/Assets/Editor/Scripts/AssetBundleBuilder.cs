using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetBundleBuilder
{
    [MenuItem("Assets/YGUI/Build AssetBundle", false, 10)]
    public static void Build()
    {
        var selectedObjects = Selection.objects;
        var directoriesPath = filterFolderPath(selectedObjects);

        
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
}