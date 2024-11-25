using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GameFrame.YAssetManage.PrefabsManage;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Button testBtn;

    private void Start()
    {
        if (testBtn != null)
        {
            testBtn.onClick.AddListener(onTestBtnClick);
        }
    }

    private void onTestBtnClick()
    {
        var content =
            File.ReadAllText(@"F:\UnityProject\YGUIGameFrameWork\My project\Shared\Prefabs\prefab_prefabs_cube.json");
        var go = PrefabSerializer.Deserialize(content);
        return;
    }
}
