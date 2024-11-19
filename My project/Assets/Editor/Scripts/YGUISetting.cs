using System;
using System.Collections.Generic;
using common;
using common.CustomDataStruct;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using util;

namespace Editor.Scripts
{
    [Serializable]
    public class YGUISettingData
    {
        public List<SerializableKVPair> labels = new List<SerializableKVPair>();
    }
    
    public class YGUISetting : EditorWindow
    {
        private const int DEFAULT_TITTLE_FONT_SIZE = 20;
        private const int DEFAULT_ITEM_FONT_SIZE = 15;
        
        private YGUISettingData _data = LocalSettingsUtil.LoadSettings<YGUISettingData>(ESettingsFilePath.YGUI);
        
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

            //资产路径
            var assetsPathContainer = CreateContainer();
            assetsPathContainer.Add(CreateItemName("Assets Path:"));
            var assetLabel = new Label
            {
                text = _data.labels.TryGetValue(GlobalConstants.YGUI_SELECT_ASSET_FOLDER_PATH, out var assetPath) ? assetPath : "",
                style = { fontSize = DEFAULT_ITEM_FONT_SIZE, }
            };
            assetsPathContainer.Add(assetLabel);
            assetsPathContainer.Add(CreateItemButton("Modify", () =>
            {
                assetLabel.text = EditorUtility.OpenFolderPanel("select asset folder", "", "");
                _data.labels.SetValue(GlobalConstants.YGUI_SELECT_ASSET_FOLDER_PATH, assetLabel.text);
            }));
            container.Add(assetsPathContainer);
            
            //存储AB包路径
            var assetBundlePathContainer = CreateContainer();
            assetBundlePathContainer.Add(CreateItemName("AssetBundle Path:"));
            var abLabel = new Label
            {
                text = _data.labels.TryGetValue(GlobalConstants.YGUI_ASSET_BUNDLE_PATH, out var abPath) ? abPath : "",
                style = { fontSize = DEFAULT_ITEM_FONT_SIZE, }
            };
            assetBundlePathContainer.Add(abLabel);
            assetBundlePathContainer.Add(CreateItemButton("Modify", () =>
            {
                abLabel.text = EditorUtility.OpenFolderPanel("select AssetBundle Save folder", "", "");
                _data.labels.SetValue(GlobalConstants.YGUI_ASSET_BUNDLE_PATH, abLabel.text);
            }));
            container.Add(assetBundlePathContainer);
            
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