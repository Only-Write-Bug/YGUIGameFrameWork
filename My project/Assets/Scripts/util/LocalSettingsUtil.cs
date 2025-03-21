﻿using System;
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

    public static class LocalSettingsUtil
    {
        private static string root = Path.Combine(Directory.GetCurrentDirectory(), "Assets\\Settings");

        private static readonly Dictionary<ESettingsFilePath, string> SettingsPathDic =
            new Dictionary<ESettingsFilePath, string>()
            {
                { ESettingsFilePath.DEFAULT, "Default" },
                { ESettingsFilePath.YGUI, "YGUI" },
            };

        
        /// <summary>
        /// 保存设置文件，会覆盖原文件所有设置
        /// </summary>
        /// <param name="settings">要保存的设置对象</param>
        /// <param name="key">设置文件路径</param>
        public static void SaveSettings(object settings, ESettingsFilePath key)
        {
            if (!SettingsPathDic.ContainsKey(key))
            {
                Logger.Error("The default setting path is empty, please check the code, Expected target key : " + key);
                return;
            }

            var targetPath = GenerateFilePath(key);
            IOUtil.CheckPath(root);

            try
            {
                var content = JsonUtility.ToJson(settings);
                File.WriteAllText(targetPath, content);
                Logger.Log("Settings saved successfully to: " + targetPath);
            }
            catch (IOException ex)
            {
                Logger.Error("Failed to save settings due to file sharing violation: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to save settings: " + ex.Message);
            }
        }

        /// <summary>
        /// 修改设置文件中的某个属性值
        /// </summary>
        /// <param name="key">设置文件路径</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">属性值</param>
        public static void ModifySetting(ESettingsFilePath key, string propertyName, string value)
        {
            if (!SettingsPathDic.ContainsKey(key))
            {
                Logger.Error("The default setting path is empty, please check the code, Expected target key : " + key);
                return;
            }

            var targetPath = Path.Combine(root, SettingsPathDic[key]) + ".json";
            IOUtil.CheckPath(root);

            try
            {
                var content = File.Exists(targetPath) ? File.ReadAllText(targetPath) : "{}";
                var obj = JsonUtility.FromJson<object>(content);
                if (obj != null)
                {
                    obj.GetType().GetProperty(propertyName)?.SetValue(obj, value);
                }
                else
                {
                    obj = new { propertyName = value };
                }
                File.WriteAllText(targetPath, JsonUtility.ToJson(obj));
                Logger.Log("Setting modified successfully: " + propertyName + " = " + value);
            }
            catch (IOException ex)
            {
                Logger.Error("Failed to modify setting due to file sharing violation: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to modify setting: " + ex.Message);
            }
        }

        public static T LoadSettings<T>(ESettingsFilePath key) where T : class, new()
        {
            var targetPath = GenerateFilePath(key);
            if (File.Exists(targetPath))
            {
                try
                {
                    var content = File.ReadAllText(targetPath);
                    return JsonUtility.FromJson<T>(content);
                }
                catch (Exception ex)
                {
                    Logger.Error("Failed to load settings due to file sharing violation: " + ex.Message);
                }
            }
            
            return new T();
        }

        public static string GenerateFilePath(ESettingsFilePath key)
        {
            return Path.Combine(root, SettingsPathDic[key]) + ".json";
        }
    }
}