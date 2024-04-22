using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PopupWindow : EditorWindow
{
    void OnGUI()
    {
        GUILayout.Label("Popup Window");
        if (GUILayout.Button("Close"))
        {
            this.Close();
        }
    }
}
