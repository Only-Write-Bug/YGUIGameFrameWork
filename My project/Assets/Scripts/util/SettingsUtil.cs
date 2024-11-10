using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace util
{
    public enum ESettingsFilePath
    {
        DEFAULT,
        YGUI,  
    }
    
    public static class SettingsUtil
    {
        private static string root = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
        private static readonly Dictionary<ESettingsFilePath, string> SettingsPathDic =
            new Dictionary<ESettingsFilePath, string>()
            {
                { ESettingsFilePath.DEFAULT , "Settings\\Default"},
                { ESettingsFilePath.YGUI, "Settings\\YGUI" },
            };

        public static void SaveSettings(object settings, ESettingsFilePath path)
        {
            if (!SettingsPathDic.ContainsKey(path))
            {
                Logger.Error("The default setting path is empty, please check the code, Expected target key : " + path);
                return;
            }
            var targetPath = Path.Combine(root, SettingsPathDic[path], ".json");
            Debug.Log(targetPath);
            JsonUtility.ToJson(settings);
        }
    }
}