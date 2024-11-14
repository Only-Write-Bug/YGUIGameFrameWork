using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

public class init : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LocalSettingsUtil.SaveSettings(new {}, ESettingsFilePath.YGUI );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
