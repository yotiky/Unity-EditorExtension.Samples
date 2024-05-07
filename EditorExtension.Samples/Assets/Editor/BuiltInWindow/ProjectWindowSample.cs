using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectWindowSample 
{
    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        // InitializeOnLoad + static コンストラクタ、もしくは InitializeOnLoadMethod で初期化する
        EditorApplication.projectWindowItemOnGUI += ProjectWindowItemOnGUI;
    }

    private static void ProjectWindowItemOnGUI(string guid, Rect selectionRect)
    {
    }
}
