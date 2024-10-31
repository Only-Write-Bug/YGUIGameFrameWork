using System;
using UnityEditor;

namespace Editor.Scripts
{
    public class YGUISetting : EditorWindow
    {
        [MenuItem("YGUI/Settings")]
        public static void OpenSettings()
        {
            EditorWindow.CreateWindow<YGUISetting>();
        }

        private void CreateGUI()
        {
            var root = rootVisualElement;
        }
    }
}