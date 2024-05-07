using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InspectorSampleComponent1))]
public class InspectorWindowSample : Editor
{
    private InspectorSampleComponent1 obj;

    private void OnEnable()
    {
        obj = target as InspectorSampleComponent1;
    }

    public override void OnInspectorGUI()
    {
        // 標準的な表示
        base.OnInspectorGUI();
    }
}