using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneSampleComponent))]
public class SceneWindowSample2 : Editor 
{
    private SceneSampleComponent obj;

    private void OnEnable()
    {
        // 有効になった時に対象を確保しておく
        obj = target as SceneSampleComponent;
    }
    
    private void OnSceneGUI()
    {
        Handles.Label(obj.transform.position + Vector3.up * 2, obj.transform.position.ToString());
    }
}
