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
                { ESettingsFilePath.DEFAULT, "Settings\\Default" },
                { ESettingsFilePath.YGUI, "Settings\\YGUI" },
            };

        /// <summary>
        /// 保存设置文件，会覆盖原文件所有设置
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="path"></param>
        public static void SaveSettings(object settings, ESettingsFilePath path)
        {
            if (!SettingsPathDic.ContainsKey(path))
            {
                Logger.Error("The default setting path is empty, please check the code, Expected target key : " + path);
                return;
            }

            var targetPath = Path.Combine(root, SettingsPathDic[path], ".json");
            var content = JsonUtility.ToJson(settings);

            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }

            File.WriteAllText(targetPath, content);
        }

        /// <summary>
        /// 修改原设定中指定的值
        /// </summary>
        /// <param name="path"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void ModifySetting(ESettingsFilePath path, string propertyName, string value)
        {
            if (!SettingsPathDic.ContainsKey(path))
            {
                Logger.Error("The default setting path is empty, please check the code, Expected target key : " + path);
                return;
            }

            var targetPath = Path.Combine(root, SettingsPathDic[path], ".json");
            var obj = File.Exists(targetPath)
                ? JsonUtility.FromJson<object>(File.ReadAllText(targetPath))
                : new { propertyName = value };
            if (File.Exists(targetPath))
                obj.GetType().GetProperty(propertyName).SetValue(obj, value);
            SaveSettings(obj, path);
        }
    }
}