using System;
using System.Collections.Generic;
using common.CustomDataStruct;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using util;

namespace Editor.Scripts
{
    public class YGUISettingData
    {
        
    }
    
    public class YGUISetting : EditorWindow
    {
        private const int DEFAULT_TITTLE_FONT_SIZE = 20;
        private const int DEFAULT_ITEM_FONT_SIZE = 15;

        private const string SELECT_ASSET_FOLDER_PATH = "select_asset_folder_path";
        
        private YGUISettingData _data = new YGUISettingData();
        
        [MenuItem("YGUI/Settings")]
        public static void OpenSettings()
        {
            EditorWindow.CreateWindow<YGUISetting>();
        }

        private void CreateGUI()
        {
            var root = rootVisualElement;
            root.style.flexDirection = FlexDirection.Column;
            root.style.alignItems = Align.Center;
            
            root.Add(InitAssetBundleSettings());
            root.Add(InitSaveBtn());
        }

        private VisualElement InitAssetBundleSettings()
        {
            var container = CreateBox();
            
            container.Add(CreateTittle("AssetBundle Settings"));

            var assetsPathContainer = CreateContainer();
            assetsPathContainer.Add(CreateItemName("Assets Path:"));
            var label = new Label
            {
                style = { fontSize = DEFAULT_ITEM_FONT_SIZE, }
            };
            assetsPathContainer.Add(label);
            assetsPathContainer.Add(CreateItemButton("Modify", () =>
            {
                label.text = EditorUtility.OpenFolderPanel("select asset folder", "", "");
            }));
            container.Add(assetsPathContainer);
            
            container.Add(CreateUnderLine());
            
            return container;
        }

        private Button InitSaveBtn()
        {
            return new Button(() =>
            {
                Debug.Log("Save Settings");
                LocalSettingsUtil.SaveSettings(_data, ESettingsFilePath.YGUI);
            })
            {
                text = "Save Settings",
                style =
                {
                    width = Length.Percent(90),
                    height = 25,
                    paddingTop = 5,
                    paddingBottom = 5,
                }
            };
        }

        private VisualElement CreateBox()
        {
            return new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Column,
                    alignItems = Align.Center,
                    width = Length.Percent(90)
                }
            };
        }

        private VisualElement CreateContainer()
        {
            return new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    alignItems = Align.Center
                }
            };
        }

        private Label CreateTittle(string content)
        {
            return new Label
            {
                text = content,
                style =
                {
                    fontSize = DEFAULT_TITTLE_FONT_SIZE,
                    color = Color.white
                }
            };
        }

        private Label CreateItemName(string name)
        {
            return new Label
            {
                text = name,
                style =
                {
                    fontSize = DEFAULT_ITEM_FONT_SIZE,
                    backgroundColor = Color.white,
                    color = Color.black
                }
            };
        }

        private Button CreateItemButton(string content, Action callback)
        {
            return new Button(() => callback?.Invoke())
            {
                text = content,
                style =
                {
                    width = 50,
                    height = 20,
                }
            };
        }

        private VisualElement CreateUnderLine()
        {
            return new VisualElement()
            {
                style =
                {
                    height = 2,
                    backgroundColor = Color.white,
                    marginTop = 3,
                    marginBottom = 3,
                    flexGrow = 1,
                    width = Length.Percent(100)
                }
            };
        }
    }
}