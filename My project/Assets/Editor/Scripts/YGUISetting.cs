using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using util;

namespace Editor.Scripts
{
    public class YGUISettingData
    {
        public Dictionary<string, string> labels = new Dictionary<string, string>();
        public Dictionary<string, string> texts = new Dictionary<string, string>();
        public Dictionary<string, bool> toggles = new Dictionary<string, bool>();
        public Dictionary<string, Enum> dropdowns = new Dictionary<string, Enum>();
    }
    
    public class YGUISetting : EditorWindow
    {
        private YGUISettingData _data = new YGUISettingData();
        [MenuItem("YGUI/Settings")]
        public static void OpenSettings()
        {
            EditorWindow.CreateWindow<YGUISetting>();
        }

        private void CreateGUI()
        {
            var root = rootVisualElement;
            root.Add(InitSaveBtn());
        }

        private Button InitSaveBtn()
        {
            return new Button(() =>
            {
                Debug.Log("Save Settings");
                LocalSettingsUtil.SaveSettings(_data, ESettingsFilePath.YGUI);
            })
            {
                text = "Save",
                style =
                {
                    flexDirection = FlexDirection.Row,
                    width = Length.Percent(30),
                    height = 25
                }
            };
        }
    }
}