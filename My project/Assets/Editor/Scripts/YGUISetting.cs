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
        private YGUISettingData _data = LocalSettingsUtil.LoadSettings<YGUISettingData>(ESettingsFilePath.YGUI);
        
        [MenuItem("YGUI/Settings")]
        public static void OpenSettings()
        {
            EditorWindow.CreateWindow<YGUISetting>();
        }

        private void CreateGUI()
        {
            var root = rootVisualElement;

            var scrollView = new ScrollView(ScrollViewMode.Vertical)
            {
                style =
                {
                    flexGrow = 1,
                    flexShrink = 1,
                    flexBasis = 0,
                    alignItems = Align.Center,
                    alignSelf = Align.Center,
                    paddingLeft = Length.Percent(10),
                    paddingRight = Length.Percent(10)
                }
            };
            scrollView.Add(InitAssetBundleSettings());
            scrollView.Add(InitSaveBtn());
            
            root.Add(scrollView);
        }

        private VisualElement InitAssetBundleSettings()
        {
            var container = UIToolKitUtil.CreateBox();
            
            container.Add(UIToolKitUtil.CreateTittle("AssetBundle Settings"));

            //资产路径
            var assetsPathContainer = UIToolKitUtil.CreateContainer();
            assetsPathContainer.Add(UIToolKitUtil.CreateItemName("Assets Path:"));
            var assetLabel = new Label
            {
                text = _data.labels.TryGetValue(GlobalConstants.YGUI_SELECT_ASSET_FOLDER_PATH, out var assetPath) ? assetPath : "",
                style = { fontSize = UIToolKitUtil.DEFAULT_ITEM_FONT_SIZE, }
            };
            assetsPathContainer.Add(assetLabel);
            assetsPathContainer.Add(UIToolKitUtil.CreateItemButton("Modify", () =>
            {
                assetLabel.text = EditorUtility.OpenFolderPanel("select asset folder", "", "");
                _data.labels.SetValue(GlobalConstants.YGUI_SELECT_ASSET_FOLDER_PATH, assetLabel.text);
            }));
            container.Add(assetsPathContainer);
            
            //存储AB包路径
            var assetBundlePathContainer = UIToolKitUtil.CreateContainer();
            assetBundlePathContainer.Add(UIToolKitUtil.CreateItemName("AssetBundle Path:"));
            var abLabel = new Label
            {
                text = _data.labels.TryGetValue(GlobalConstants.YGUI_ASSET_BUNDLE_PATH, out var abPath) ? abPath : "",
                style = { fontSize = UIToolKitUtil.DEFAULT_ITEM_FONT_SIZE, }
            };
            assetBundlePathContainer.Add(abLabel);
            assetBundlePathContainer.Add(UIToolKitUtil.CreateItemButton("Modify", () =>
            {
                abLabel.text = EditorUtility.OpenFolderPanel("select AssetBundle Save folder", "", "");
                _data.labels.SetValue(GlobalConstants.YGUI_ASSET_BUNDLE_PATH, abLabel.text);
            }));
            container.Add(assetBundlePathContainer);
            
            //生成AB包按钮
            container.Add(new Button(() =>
            {
                if (!string.IsNullOrEmpty(assetLabel.text))
                {
                    AssetBundleBuilder.GeneraAssetBundle(new string[]{assetLabel.text});
                }
                else
                {
                    Debug.LogWarning("The path of the asset to be generated for the AB package is not specified");
                }
            })
            {
                text = "Generate AB",
                style =
                {
                    width = Length.Percent(50),
                    height = 20,
                    fontSize = UIToolKitUtil.DEFAULT_ITEM_FONT_SIZE
                }
            });
            
            container.Add(UIToolKitUtil.CreateUnderLine());
            
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
    }
}